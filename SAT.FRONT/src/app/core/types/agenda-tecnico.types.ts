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
    codFilial?: number;
    codTecnicos?: string;
    indAtivo?: number;
    codOS?: number;
    tipo?: AgendaTecnicoTipoEnum;
    inicio?: string;
    fim?: string;
    pas?: string;
    indFerias?: number;
}

export interface AgendaTecnicoData extends Meta
{
    items: AgendaTecnico[]
};

export class AgendaTecnico
{
    codAgendaTecnico?: number;
    tipo?: AgendaTecnicoTipoEnum;
    titulo?: string;
    cor?: string;
    codTecnico: number;
    tecnico?: Tecnico;
    codOS?: number;
    ordemServico?: OrdemServico;
    inicio: string;
    fim: string;
    indAgendamento: number;
    indAtivo: number;
    codUsuarioManut?: string;
    dataHoraManut?: string;
    dataHoraCad: string;
    codUsuarioCad: string;
}

export enum AgendaTecnicoTipoEnum
{
    OS = 1,
    INTERVALO = 2,
    PONTO = 3,
    FIM_EXPEDIENTE = 4,
    CHECKIN = 5,
    CHECKOUT = 6,
    INTENCAO = 7
}

export enum AgendaTecnicoOrdenationEnum
{
    FIM_SLA = 1,
    MENOR_TRAGETORIA = 2
}

export interface MbscAgendaTecnicoCalendarEvent extends MbscCalendarEvent
{
    codAgendaTecnico?: number;
    codOS?: number;
    agendaTecnico?: AgendaTecnico;
    ordemServico?: OrdemServico;
    cliente?: string;
    equipamento?: string;
}

export interface TecnicoMaisProximo
{
    minDistancia: number;
    codTecnicoMinDistancia: number
    ultimoAtendimentoTecnico: MbscAgendaTecnicoCalendarEvent;
    message: string;
}

export interface ViewAgendaTecnicoEvento {
    codAgendaTecnico: number | null;
    cor: string;
    titulo: string;
    editavel: boolean;
    tipo: AgendaTecnicoTipoEnum;
    codFilial: number;
    codUsuario: string;
    codTecnico: number;
    nome: string;
    fonePerto: string;
    codOS: number | null;
    codStatusServico: number | null;
    nomeStatusServico: string;
    nomTipoIntervencao: string;
    nomeLocal: string;
    clientes: string;
    inicio: string | null;
    fim: string | null;
    dataHoraLimiteAtendimento: string | null;
    dataAgendamento: string | null;
    dataHoraCad: string;
    codUsuarioCad: string;
    indAtivo: number;
    cliente: string;
    equipamento: string;
    checkin: string;
    checkout: string;
}

export interface ViewAgendaTecnicoRecurso {
    id: number;
    nome: string;
    codUsuario: string;
    fonePerto: string;
    qtdChamadosTransferidos: number;
    qtdChamadosAtendidos: number;
    eventos: ViewAgendaTecnicoEvento[];
}