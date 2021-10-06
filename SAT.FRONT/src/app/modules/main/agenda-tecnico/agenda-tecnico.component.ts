import { Component, OnInit } from '@angular/core';
import { setOptions, MbscCalendarEvent, localePtBR, Notifications, MbscEventcalendarOptions, MbscEventcalendarView } from '@mobiscroll/angular';
import { NominatimService } from 'app/core/services/nominatim.service';
import { OrdemServicoService } from 'app/core/services/ordem-servico.service';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { Coordenada } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico, OrdemServicoData } from 'app/core/types/ordem-servico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import moment, { Moment } from 'moment';
import Enumerable from 'linq';

setOptions({
    locale: localePtBR,
    theme: 'windows',
    themeVariant: 'light',
    clickToCreate: true,
    dragToCreate: true,
    dragToMove: true,
    dragToResize: true
});

@Component({
    selector: 'app-agenda-tecnico',
    templateUrl: './agenda-tecnico.component.html',
    styleUrls: ['./agenda-tecnico.component.scss'],
})
export class AgendaTecnicoComponent implements OnInit {

    tecnicos: Tecnico[] = [];
    chamados: OrdemServico[] = [];

    constructor(
        private _notify: Notifications,
        private _tecnicoSvc: TecnicoService,
        private _osSvc: OrdemServicoService,
        private _nominatimSvc: NominatimService
    ) { }

    calendarOptions: MbscEventcalendarOptions = {
        view: {
            timeline: {
                type: 'day',
                allDay: false,
                startDay: 1,
                startTime: '08:00',
                endTime: '19:00'
            }
        },
        dragToMove: true,
        externalDrop: true,
        onEventCreate: (args, inst) => 
        {
            if (this.hasOverlap(args, inst)) {
                this._notify.toast({
                    message: 'Os eventos não podem se sobrepor.'
                });
                return false;
            }
        },
        onEventUpdate: (args, inst) => 
        {
            if (this.hasOverlap(args, inst)) {
                this._notify.toast({
                    message: 'Os eventos não podem se sobrepor.'
                });
                return false;
            }
        }
    };

    hasOverlap(args, inst) 
    {
        var ev = args.event;
        var events = inst.getEvents(ev.start, ev.end).filter(e => e.resource == ev.resource);
        return events.length > 0;
    }

    view: MbscEventcalendarView = {
        
    };

    events: MbscCalendarEvent[] = [];
    resources = [];
    externalEvents = [];
    inicioExpediente = moment().set({hour:8,minute:0,second:0,millisecond:0});
    fimExpediente = moment().set({hour:18,minute:0,second:0,millisecond:0});
    inicioAlmoco = moment().set({hour:12,minute:0,second:0,millisecond:0});
    fimAlmoco = moment().set({hour:13,minute:0,second:0,millisecond:0});

    retreatData = {
        title: 'Team retreat',
        color: '#1064b0',
        start: moment(),
        end: moment().add(60, 'minutes')
    };

    meetingData = {
        title: 'QA meeting',
        color: '#cf4343',
        start: moment(),
        end: moment().add(60, 'minutes')
    };

    ngOnInit(): void {
        this.obterTecnicosEChamadosTransferidos();
        this.obterChamadosAbertos();
    }

    private async obterTecnicosEChamadosTransferidos() {
        const tecnicos = await this._tecnicoSvc.obterPorParametros({
            indAtivo: 1,
            codFilial: 4,
            codPerfil: 35,
            sortActive: 'nome',
            sortDirection: 'asc'
        }).toPromise();

        this.resources = tecnicos.items.map(tecnico => {
            return {
                id: tecnico.codTecnico,
                name: tecnico.nome,
                img: `https://sat.perto.com.br/DiretorioE/AppTecnicos/Fotos/${tecnico.usuario.codUsuario}.jpg`,
            }
        });
        
        const chamados = await this._osSvc.obterPorParametros({
            codFiliais: "4",
            codStatusServicos: "8",
            sortActive: 'dataHoraTransf',
            sortDirection: 'asc'
        }).toPromise();

        this.carregaSugestaoAlmoco(tecnicos.items);
        this.carregaEventos(chamados.items);
    }

    private carregaEventos(chamados: OrdemServico[])
    {
        this.events = this.events.concat(Enumerable.from(chamados).groupBy(os => os.codTecnico).selectMany(osPorTecnico =>
        {
            var mediaTecnico = 30;
            var ultimoEvento: MbscCalendarEvent;

            return (Enumerable.from(osPorTecnico).orderBy(os => os.dataHoraTransf).toArray().map(os =>
            {
                var start = ultimoEvento != null ? moment(ultimoEvento.end).add(mediaTecnico, 'minutes') : this.inicioExpediente;

                // se começa durante a sugestão de intervalo ou deopis das 18h
                if (start.isBetween(this.inicioAlmoco, this.fimAlmoco))
                {
                    start = moment(this.fimAlmoco);
                }
                else if (start.hour() >= this.fimExpediente.hour())
                {
                    start = moment(this.inicioExpediente).add(1, 'day');
                }

                // se termina durante a sugestao de intervalo
                var end: Moment = moment(start).add(mediaTecnico, 'minutes');
                if (end.isBetween(this.inicioAlmoco, this.fimAlmoco))
                {
                    start = moment(this.fimAlmoco);
                    end = moment(start).add(mediaTecnico, 'minutes');
                }

                var evento: MbscCalendarEvent = 
                {
                    start: start,
                    end: end,
                    title: os.codOS.toString(),
                    color: '#388E3C',
                    editable: true,
                    resource: os.tecnico.codTecnico,
                }

                ultimoEvento = evento;
                return evento;
            }))
        }).toArray());
    }

    private carregaSugestaoAlmoco(tecnicos: Tecnico[])
    {
        this.events = this.events.concat(Enumerable.from(tecnicos).select(tecnico =>
        {
            var start = this.inicioAlmoco;
            var end = this.fimAlmoco;
            var evento: MbscCalendarEvent = 
            {
                start:start,
                end: end,
                title: "INTERVALO",
                color: '#808080',
                editable: true,
                resource: tecnico.codTecnico,
            }
            return evento;
        }).toArray());
    }

    private async obterChamadosAbertos() {
        const data = await this._osSvc.obterPorParametros({
            codStatusServicos: "1",
            codFiliais: "4"
        }).toPromise();

        this.externalEvents = data.items.map(os => {
            return {
                title: os.codOS.toString(),
                color: '#1064b0',
                start: moment(),
                end: moment().add(60, 'minutes')
            }
        })
    }

    onCellDoubleClick(event: any): void {
        this._notify.alert({
            title: 'Click',
            message: event.date + ' resource ' + event.resource
        });
    }

    private async addEvents(chamados: OrdemServicoData): Promise<MbscCalendarEvent[]>
    {
       return await Promise.all(chamados.items.map(async os => 
       {
            return this.calculaDeslocamento(os).then(inicio =>
                {
                    const fim = moment(inicio).add(60, 'minutes');
                    return {
                        start: inicio,
                        end:  fim,
                        title: os.codOS.toString(),
                        color: '#388E3C',
                        editable: true,
                        resource: os.tecnico.codTecnico,
                        } 
                });
        }));
    }

    private async calculaDeslocamento(os: OrdemServico)
    {
        var origem: Coordenada = new Coordenada();
        var destino: Coordenada = new Coordenada();
        var inicioDeslocamento: Moment = moment(os.dataHoraTransf);

        // Verificar se ele já possui algum chamado agendado, pegar proximo horario disponivel
        var ultimoEvento = this.chamados.filter(i => i.codTecnico === os.tecnico.codTecnico && i.codOS != os.codOS)[0];

        if(ultimoEvento != null)
        {
            // E se for horário de almoço? qual a coordenada?
            origem.cordenadas = [ultimoEvento.localAtendimento.latitude, ultimoEvento.localAtendimento.longitude];
            inicioDeslocamento = moment(ultimoEvento.dataHoraTransf);
        }
        else
        {
            // Se o técnio não possui nada agendado
            // 44 técnicos ativos não possuem informações de latitude e longitude
            var tecnico = this.tecnicos.find(t => t.codTecnico == os.codTecnico);

            if (tecnico == null)
                return moment(os.dataHoraTransf);

            origem.cordenadas = [tecnico.latitude, tecnico.longitude];
        }

        destino.cordenadas = [os.localAtendimento.latitude, os.localAtendimento.longitude]; 

        // Se todas as coordenadas estiverem disponiveis, calcula a distância.
        if (origem.cordenadas[0] && origem.cordenadas[1] && destino.cordenadas[0] && destino.cordenadas[1])
        {
            var deslocamento = await this._nominatimSvc.deslocamentoEmMinutos(origem, destino);
            return inicioDeslocamento.add(deslocamento, 'minutes');
        }
        else 
        {
            // Se não possui todas as coordenadas, retorna a hora de transferencia + meia hora
            return inicioDeslocamento.add(30, 'minutes');
        }
    }
}