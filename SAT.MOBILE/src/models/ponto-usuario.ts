import { PontoPeriodo } from "./ponto-periodo";

export class PontoUsuario {
    codPontoUsuario: number;
    pontoPeriodo: PontoPeriodo;
    codUsuario: string;
    dataHoraRegistro: string;
    dataHoraEnvio: string;
    latitude: number;
    longitude: number;
    indAtivo: number;
    correcaoHabilitada: number;
    sincronizado: boolean;
}