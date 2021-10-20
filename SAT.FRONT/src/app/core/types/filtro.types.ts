export interface Filtro
{
    parametros: Parametros;
    nome: string
}

export interface Parametros 
{
    codFiliais?: string,
    codAutorizadas?: string,
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
    pa?: number,
    pontosEstrategicos?: number[],
    qtdPaginacaoLista?: number,
    sortActive?: string,
    sortDirection?: string
}