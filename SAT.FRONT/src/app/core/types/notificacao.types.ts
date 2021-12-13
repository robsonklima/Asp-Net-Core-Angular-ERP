import { Meta, QueryStringParameters } from "./generic.types";

export interface Notificacao
{
    codNotificacao?: number;
    titulo?: string;
    descricao?: string;
    icone?: string;
    lida?: number;
    link?: string;
    dataHoraCad?: string;
    codUsuarioCad?: string;
    dataHoraManut?: string;
    codUsuarioManut?: string;
    indAtivo: number;
    useRouter?: number;
    codUsuario?: string;
}

export interface NotificacaoData extends Meta
{
    items: Notificacao[];
};

export interface NotificacaoParameters extends QueryStringParameters
{
    codUsuario: string;
};
