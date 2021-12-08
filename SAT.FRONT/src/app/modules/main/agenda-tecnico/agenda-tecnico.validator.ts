import { Injectable } from '@angular/core';
import { HaversineService } from 'app/core/services/haversine.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Coordenada, MbscAgendaTecnicoCalendarEvent, TecnicoOMaisProximo } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import Enumerable from 'linq';
import moment, { Moment } from 'moment';
import { AgendaTecnicoFormatter } from './agenda-tecnico.formatter';

@Injectable({
    providedIn: 'root'
})

export class AgendaTecnicoValidator
{
    constructor (private _tecnicoService: TecnicoService,
        private _haversineSvc: HaversineService,
        private _formatter: AgendaTecnicoFormatter) { }

    /**
     * valida se a região do chamado e a região do técnico são iguais
     */
    public async isTecnicoDaRegiaoDoChamado(os: OrdemServico, codTecnico: number)
    {
        var tecnico = (await this._tecnicoService.obterPorCodigo(codTecnico).toPromise());

        if (tecnico?.codRegiao == os.codRegiao)
            return true;

        return false;
    }

    public isTecnicoOMaisProximo(os: OrdemServico, tecnicos: Tecnico[], events: MbscAgendaTecnicoCalendarEvent[], codTecnico: number)
    {
        var codTecnicosFiilial = Enumerable.from(tecnicos).select(i => i.codTecnico);

        var ultimoAtendimentoTecnico =
            Enumerable.from(events)
                .where(i => i.resource == codTecnico)
                .orderByDescending(i => i.end)
                .firstOrDefault();

        var minDistancia: number =
            this.calculaDeslocamentoEmMinutos(os, ultimoAtendimentoTecnico.ordemServico);

        var codTecnicoMinDistancia: number = codTecnico;

        var ultimosAtendimentosAgendados =
            Enumerable.from(events)
                .where(i => codTecnicosFiilial.contains(+i.resource) && i.ordemServico != null)
                .groupBy(i => i.resource)
                .select(i => i.maxBy(i => i.end))
                .toArray();

        ultimosAtendimentosAgendados.forEach(i => 
        {
            var distancia = this.calculaDeslocamentoEmMinutos(os, i.ordemServico);

            if (distancia < minDistancia)
            {
                minDistancia = distancia;
                codTecnicoMinDistancia = +i.resource;
                ultimoAtendimentoTecnico = i;
            }
        });

        if (codTecnicoMinDistancia != codTecnico)
        {
            var nomeTecnico = ultimoAtendimentoTecnico.ordemServico?.tecnico?.nome;
            var tec: TecnicoOMaisProximo =
            {
                minDistancia: minDistancia,
                codTecnicoMinDistancia: codTecnicoMinDistancia,
                ultimoAtendimentoTecnico: ultimoAtendimentoTecnico,
                message: this.getTecnicoOMaisProximoMessage(nomeTecnico, minDistancia, os)
            }

            return tec
        }
        else
            return null;
    }

    private getTecnicoOMaisProximoMessage(nomeTecnico: string, minDistancia: number, os: OrdemServico)
    {
        if (minDistancia == 0)
            return `O técnico ${nomeTecnico} encontra-se no local de atendimento do chamado ${os?.codOS}. Deseja transferir este chamado para ${nomeTecnico}?`;


        return `O técnico ${nomeTecnico} encontra-se a ${minDistancia.toFixed(2)} minutos do local de atendimento do chamado ${os?.codOS}. Deseja transferir este chamado para ${nomeTecnico}?`;
    }

    public calculaDeslocamentoEmMinutos(os: OrdemServico, osAnterior: OrdemServico): number
    {
        var origem: Coordenada = new Coordenada();
        var destino: Coordenada = new Coordenada();

        // se ele já estava atendendo algum chamado, parte das coordenadas deste chamado
        if (osAnterior != null)
            origem.cordenadas = [osAnterior.localAtendimento?.latitude, osAnterior.localAtendimento?.longitude];
        // Se o técnico não possui nada agendado, parte do endereoç deste
        else
            origem.cordenadas = [os.tecnico?.latitude, os.tecnico?.longitude];

        destino.cordenadas = [os.localAtendimento?.latitude, os.localAtendimento?.longitude];

        return this._haversineSvc.getDistanceInMinutesPerKm(origem, destino, 50);
    }

    public isOnVacation(t: Tecnico): boolean
    {
        if (!t.indFerias)
            return false;

        // if (moment(t.dtFeriasInicio) >= moment() && moment(t.dtFeriasFim) <= moment())
        //   return true;

        return true;
    }

    public inicioIntervalo(reference: Moment = moment())
    {
        return moment(reference).set({ hour: 12, minute: 0, second: 0, millisecond: 0 });
    }

    public fimIntervalo(reference: Moment = moment())
    {
        return moment(reference).set({ hour: 13, minute: 0, second: 0, millisecond: 0 });
    }

    public inicioExpediente(reference: Moment = moment())
    {
        return moment(reference).set({ hour: 8, minute: 0, second: 0, millisecond: 0 });
    }

    public fimExpediente(reference: Moment = moment())
    {
        return moment(reference).set({ hour: 18, minute: 0, second: 0, millisecond: 0 });
    }

    public hasOverlap(args, inst)
    {
        var ev = args.event;
        var events = inst.getEvents(ev.start, ev.end).filter(e => e.resource == ev.resource && e.id != ev.id && (e.ordemServico != null || ev.ordemServico != null));
        return events.length > 0;
    }

    public invalidInsert(args)
    {
        //não pode inserir evento anterior à linha do tempo
        var now = moment();
        return moment(args.event.start) < now;
    }

    public isTechnicianInterval(args)
    {
        return args.event.title === "INTERVALO";
    }

    public invalidMove(args)
    {
        //não pode mover evento posterior a linha do tempo para antes da linha do tempo
        var now = moment();
        return moment(args.oldEvent.start) > now && moment(args.event.start) < now;
    }

    public hasChangedResource(args)
    {
        return args.event.resource != args.oldEvent.resource;
    }

    public validateEvents(events: MbscAgendaTecnicoCalendarEvent[]): void
    {
        var now = moment();
        Enumerable.from(events).where(e => e.ordemServico != null).forEach(e =>
        {
            if (moment(e.end) < now)
                e.color = this._formatter.getStatusColor(e.ordemServico.statusServico?.codStatusServico);
        });
    }
}