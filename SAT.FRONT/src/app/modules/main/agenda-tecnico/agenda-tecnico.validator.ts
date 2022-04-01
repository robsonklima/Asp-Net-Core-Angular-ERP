import { Injectable } from '@angular/core';
import { DateTimeExtensions } from 'app/core/extensions/date-time.extensions';
import { TecnicoService } from 'app/core/services/tecnico.service';
import { AgendaTecnico, AgendaTecnicoTipoEnum } from 'app/core/types/agenda-tecnico.types';
import { OrdemServico } from 'app/core/types/ordem-servico.types';
import { Tecnico } from 'app/core/types/tecnico.types';
import moment, { Moment } from 'moment';

@Injectable({
    providedIn: 'root'
})

export class AgendaTecnicoValidator
{
    constructor (
        private _tecnicoService: TecnicoService,
        private _dateTimeExtensions: DateTimeExtensions
    ) {}

    public async isTecnicoDaRegiaoDoChamado(os: OrdemServico, codTecnico: number)
    {
        var tecnico = (await this._tecnicoService.obterPorCodigo(codTecnico).toPromise());

        if (tecnico?.codRegiao == os.codRegiao)
            return true;

        return false;
    }

    public isOnVacation(t: Tecnico): boolean
    {
        if (!t.indFerias)
            return false;

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
        var events = inst.getEvents(ev.start, ev.end).filter(e => (e.resource == ev.resource && e.id != ev.id));
        events = events.filter(e => e.agendaTecnico.tipo == AgendaTecnicoTipoEnum.OS);
        return events.length > 0;
    }

    public invalidInsert(args)
    {
        var now = moment();
        return moment(args.event.start) < now;
    }

    public isTechnicianInterval(event)
    {
        return event.agendaTecnico.tipo === AgendaTecnicoTipoEnum.INTERVALO;
    }

    public invalidMove(args)
    {
        var now = moment();
        return moment(args.oldEvent.start) > now && moment(args.event.start) < now;
    }

    public cantChangeInterval(args)
    {
        return args.event.resource != args.oldEvent.resource && (args.event.agendaTecnico.tipo === AgendaTecnicoTipoEnum.INTERVALO || args.oldEvent.agendaTecnico.tipo === AgendaTecnicoTipoEnum.INTERVALO);
    }

    public getTypeColor(type: AgendaTecnicoTipoEnum): string
    {
        switch (type)
        {
            case AgendaTecnicoTipoEnum.OS:
                return "#009000";
            case AgendaTecnicoTipoEnum.PONTO:
                return "#C8C8C8C8";
            case AgendaTecnicoTipoEnum.INTERVALO:
                return "#C8C8C8C8";
        }
    }

    public agendamentoColor(): string
    {
        return '#381354';
    }

    public lateColor(): string
    {
        return '#ff4c4c';
    }

    public getRealocationStatusColor(ag: AgendaTecnico, referenceTime: Moment): string
    {
        if (ag.indAgendamento == 1)
            return this.agendamentoColor();
        else if (referenceTime < moment())
            return this.lateColor();
        return this.getTypeColor(AgendaTecnicoTipoEnum.OS);
    }

    private getTimeFromMins(mins)
    {
        return this._dateTimeExtensions.getTimeFromMins(mins);
    }
}