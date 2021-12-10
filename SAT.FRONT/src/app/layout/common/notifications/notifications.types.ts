export interface Notificacao
{
    codNotificacao: number;
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
}
