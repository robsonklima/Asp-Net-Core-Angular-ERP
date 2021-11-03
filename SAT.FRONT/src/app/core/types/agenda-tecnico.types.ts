import { Meta } from "@angular/platform-browser";
import { MbscCalendarEvent } from "@mobiscroll/angular";
import { QueryStringParameters } from "./generic.types";
import { OrdemServico } from "./ordem-servico.types";
import { Tecnico } from "./tecnico.types";

export type CalendarDrawerMode = 'over' | 'side';

export type CalendarEventPanelMode = 'view' | 'add' | 'edit';
export type CalendarEventEditMode = 'single' | 'future' | 'all';

export interface CalendarSettings
{
    dateFormat: 'DD/MM/YYYY' | 'MM/DD/YYYY' | 'YYYY-MM-DD' | 'll';
    timeFormat: '12' | '24';
    startWeekOn: 6 | 0 | 1;
}

export interface CalendarWeekday
{
    abbr: string;
    label: string;
    value: string;
}

export interface AgendaTecnicoParameters extends QueryStringParameters
{
    pa?: number;
    codFiliais?: string;
    codOS?: number;
    codTecnico?: number;
    inicio?: string;
    tipo?: string;
    fim?: string;
    data?: string;
}

export interface AgendaTecnicoData extends Meta
{
    items: AgendaTecnico[]
};

export class Coordenada
{
    cordenadas: [string, string];
}

export class AgendaTecnico
{
    codAgendaTecnico?: number;
    tipo?: string;
    codTecnico: number;
    tecnico?: Tecnico;
    codOS?: number;
    ordemServico?: OrdemServico;
    ultimaAtualizacao: string;
    inicio: string;
    fim: string;
}

export interface MbscAgendaTecnicoCalendarEvent extends MbscCalendarEvent
{
    ordemServico?: OrdemServico;
    agendaTecnico?: AgendaTecnico;
    codAgendaTecnico?: number;
}