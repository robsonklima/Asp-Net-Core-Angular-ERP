export class UsuarioSeguranca {
    codUsuario: string;
    senhaBloqueada?: boolean;
    senhaExpirada?: boolean;
    quantidadeTentativaLogin?: number;
    codUsuarioCad?: string;
    codUsuarioManut?: string;
    dataHoraCad?: Date;
    dataHoraManut?: Date;
    indAtivo: number;
}