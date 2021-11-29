import { Meta, QueryStringParameters } from "./generic.types";

export class SLA {
        codSLA: number;
        nomeSLA: string;
        descSLA: string;
        horarioInicio: string | null;
        horarioFim: string | null;
        tempoInicio: number | null;
        tempoReparo: number | null;
        tempoSolucao: number | null;
        indAgendamento: boolean | null;
        indHorasUteis: boolean | null;
        indSegunda: boolean | null;
        indTerca: boolean | null;
        indQuarta: boolean | null;
        indQuinta: boolean | null;
        indSexta: boolean | null;
        indSabado: boolean | null;
        indDomingo: boolean | null;
        indFeriado: boolean | null;
        dataCadastro: string | null;
        codUsuarioCad: string;
        dataManutencao: string | null;
        codUsuarioManutencao: string;
        indFeriadoEstadual: boolean | null;
        indFeriadoNacional: boolean | null;
        indFeriadoMunicipal: boolean | null;
        indRestricaoAcesso: boolean | null;
        indRestricaoHorario: boolean | null;
        indSemat: boolean | null;
        indPontoExterno: boolean | null;
        indPontoEstrategico: number | null;
        tempoRestricaoAcesso: number | null;
        tempoRestricaoHorario: number | null;
        tempoSemat: number | null;
        tempoPontoExterno: number | null;
        tempoPontoEstrategico: number | null;
        indPeriodoAgendamento: number | null;
  
}

export interface SLAData extends Meta {
    items: SLA[]
};

export interface SLAParameters extends QueryStringParameters {
    NomeSLA?: string;
    TempoInicio?: number;
    TempoReparo?: number;
    TempoSolucao?: number;
};