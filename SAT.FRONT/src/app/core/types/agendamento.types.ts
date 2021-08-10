import { Meta, QueryStringParameters } from "./generic.types";
import { MotivoAgendamento } from "./motivo-agendamento.types";

export class Agendamento {
    codAgendamento?: number;
    codMotivo: number;
    motivoAgendamento?: MotivoAgendamento;
    codOS: number;
    dataAgendamento: string;
    codUsuarioAgendamento: string;
    dataHoraUsuAgendamento: string;
}

export interface AgendamentoData extends Meta {
    agendamentos: Agendamento[]
};

export interface AgendamentoParameters extends QueryStringParameters {
    codAgendamento?: number;
};