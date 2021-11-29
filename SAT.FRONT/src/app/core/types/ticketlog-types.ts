export interface TicketLogPedidoCredito
{
    codTicketLogPedidoCredito?: number,
    codDespesaPeriodoTecnico: number,
    valor?: number,
    numeroCartao?: string,
    indProcessado?: number,
    dataHoraProcessamento?: string,
    observacao?: string,
    codUsuarioCad?: string,
    dataHoraCad?: string
}

export interface TicketLogUsuarioCartaoPlaca
{
    codTicketLogUsuarioCartaoPlaca: number,
    numeroCartao: string,
    nomeResponsavel: string,
    placa: string,
    veiculoCidade: string,
    dataAtivacao: string,
    situacao: string,
    veiculoUF: string,
    descricaoTipoCombustivel: string,
    descricaoTipoFrota: string,
    valorReservado: number,
    saldo: number,
    descricaoModeloVeiculo: string,
    temporario: string,
    veiculoFabricante: string,
    compras: number,
    controlaHodometro: string,
    limiteAtual: number,
    codigoUsuarioCartao: number,
    situacaoVeiculo: string,
    trackOnline: string,
    controlaHorimetro: string,
    dataHoraCad: string,
    dataHoraManut: string,
}