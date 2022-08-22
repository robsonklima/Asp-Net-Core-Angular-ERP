import { Meta, QueryStringParameters } from "./generic.types";

export interface MensagemTecnico {
    codMensagemTecnico: number;
    assunto: string;
    mensagem: string;
    codUsuarioDestinatario: string;
    codUsuarioCad: string;
    dataHoraCad: string | null;
    indLeitura: number | null;
    dataHoraLeitura: string | null;
    indAtivo: number | null;
    selecionado?: boolean;
}

export interface MensagemTecnicoData extends Meta {
    items: MensagemTecnico[];
};

export interface MensagemTecnicoParameters extends QueryStringParameters {
};