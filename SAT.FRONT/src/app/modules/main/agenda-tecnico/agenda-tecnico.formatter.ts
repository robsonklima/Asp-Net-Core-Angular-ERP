import { Injectable } from '@angular/core';
import { MbscAgendaTecnicoCalendarEvent } from 'app/core/types/agenda-tecnico.types';
import Enumerable from 'linq';
import moment from 'moment';

@Injectable({
    providedIn: 'root'
})
export class AgendaTecnicoFormatter
{
    constructor () { }

    public getStatusColor(statusOS: number): string
    {
        switch (statusOS)
        {
            case 1: //aberto
                return "#ff4c4c";
            case 8: //transferido
                return "#ff4c4c";
            case 3: //fechado
                return "#7f7fff";
        }
    }

    public getInterventionColor(tipoIntervencao: number): string
    {
        switch (tipoIntervencao)
        {
            case 1: //alteracao engenharia
                return "#067A52";
            case 2: //corretiva
                return "#3FC283";
            case 4: //preventiva
                return "#87E9A9";
            default:
                return "#D7F4D2";
        }
    }

    public getEventColor(args)
    {
        return moment(args.event.end) > moment() ?
            this.getInterventionColor(args.event.ordemServico?.tipoIntervencao?.codTipoIntervencao)
            : this.getStatusColor(args.event.ordemServico?.statusServico?.codStatusServico);
    }

    public updateEventColor(events: MbscAgendaTecnicoCalendarEvent[], args)
    {
        if (args.event.ordemServico?.codOS > 0)
        {
            var event = Enumerable.from(events).firstOrDefault(e => e.codAgendaTecnico == args.event.codAgendaTecnico);
            event.color = this.getEventColor(args);
        }
    }
}