import { Contrato } from "./contrato.types";
import { Meta, QueryStringParameters } from "./generic.types";
import { InstalacaoPagtoInstal } from "./instalacao-pagto-instal.types";

export class InstalacaoPagto {
    codInstalPagto: number;
    codContrato?: number;
    dataPagto?: string;
    vlrPagto?: number;
    obsPagto?: string; 
    codUsuarioCad?: string; 
    dataHoraCad?: string; 
    codUsuarioManut?: string; 
    dataHoraManut?: string; 
    contrato?: Contrato;
    instalacoesPagtoInstal?: InstalacaoPagtoInstal[] = [];   
}

export interface InstalacaoPagtoData extends Meta {
    items: InstalacaoPagto[];
};

export interface InstalacaoPagtoParameters extends QueryStringParameters {
    codInstalPagto?: number;
    codContrato?: number;
    codContratos?: string;
    codCliente?: number;
    codTipoContratos?: string;
    dataPagto?: string;
    vlrPagto?: number;
};