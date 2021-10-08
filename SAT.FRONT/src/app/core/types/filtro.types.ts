export interface Filtro {
    parametros: Parametros;
    nome: string
}

export interface Parametros 
{
    codFiliais?: number[],
    codTiposIntervencao?: number[],
    codClientes?: number[],
    codStatusServicos?: number[],
    codOS?: number,
    numOSCliente?: string,
    numOSQuarteirizada?: string,
    dataAberturaInicio?: string,
    dataAberturaFim?: string,
    dataFechamentoInicio?: string,
    dataFechamentoFim?: string,
    pa?: number
    qtdPaginacaoLista?: number
}