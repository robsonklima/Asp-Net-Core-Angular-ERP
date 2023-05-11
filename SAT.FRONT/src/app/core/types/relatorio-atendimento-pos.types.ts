import { Meta, QueryStringParameters } from "./generic.types";

export interface RelatorioAtendimentoPOS {
    codRat: number;
    codRATbanrisul: number;
    numSerieInst: string;
    numSerieRet: string;
    rede: string;
    codTipoComunicacao: number | null;
    numeroChipInstalado: string;
    codOperadoraTelefoniaChipInstalado: number | null;
    numeroChipRetirado: string;
    codOperadoraTelefoniaChipRetirado: number | null;
    codMotivoComunicacao: number | null;
    obsMotivoComunicacao: string;
    atendimentoRealizadoPorTelefone: boolean | null;
    codEquipRet: number | null;
    codEquipInst: number | null;
    indSmartphone: number | null;
    codDefeitoPos: number | null;
    codMotivoCancelamento: number | null;
    obsMotivoCancelamento: string;
}

export interface RelatorioAtendimentoPOSData extends Meta {
    items: RelatorioAtendimentoPOS[];
};

export interface RelatorioAtendimentoPOSParameters extends QueryStringParameters {
};