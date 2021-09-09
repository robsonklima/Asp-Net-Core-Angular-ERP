import { Meta } from "@angular/platform-browser";
import { QueryStringParameters } from "./generic.types";
import { Tecnico } from "./tecnico.types";

export interface Calendar
{
    id: string;
    title: string;
    color: string;
    visible: boolean;
    dataHoraCad: string;
    codTecnico: number;
    eventos: CalendarEvent[]
}

export type CalendarDrawerMode = 'over' | 'side';

export interface CalendarEvent
{
    id: string;
    calendarId: string;
    codOS?: number;
    duration: number;
    title: string;
    description: string;
    start: string | null;
    end: string | null;
    dataHoraCad?: string;
    codUsuarioCad?: string;
    allDay?: boolean;
    recurrence?: string;
    recurringEventId?: string | null;
    isFirstInstance?: boolean;
    dataHoraManut?: string;
    codUsuarioManut?: string;
}

export interface CalendarEventException
{
    id: string;
    eventId: string;
    exdate: string;
}

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

export interface AgendaTecnicoParameters extends QueryStringParameters {
    pa?: number;
    codFilial?: number;
    codTecnico?: number;
}

export interface AgendaTecnicoData extends Meta {
    items: Calendar[]
};
