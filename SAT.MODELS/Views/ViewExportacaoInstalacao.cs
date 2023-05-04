﻿using System;

namespace SAT.MODELS.Views
{
    public class ViewExportacaoInstalacao
    {
        public int? CodInstalacao { get; set; }
        public string Filial { get; set; }
        public string Autorizada { get; set; }
        public string Regiao { get; set; }
        public string LoteImportacao { get; set; }
        public DateTime? DataRemessa { get; set; }
        public string Contrato { get; set; }
        public string PedidoCompra { get; set; }
        public string SuperE { get; set; }
        public string CMA { get; set; }
        public string Lote { get; set; }
        public string Terceirizado { get; set; }
        public string TipoDep { get; set; }
        public string TipoNovo { get; set; }
        public string NSerie { get; set; }
        public string NumeroTAANovo { get; set; }
        public string PrefixoSB { get; set; }
        public string Dependencia { get; set; }
        public string CNPJ { get; set; }
        public string Logradouro { get; set; }
        public string Municipio { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public DateTime? PrevisaoEntrega { get; set; }
        public DateTime? DtConfirmadaEntrega { get; set; }
        public string NFRemessa { get; set; }
        public DateTime? NFRemessaDataExpedicao { get; set; }
        public DateTime? DtExpedicao { get; set; }
        public string TransportadoraEntrega { get; set; }
        public string AgEntrega { get; set; }
        public string NomeLocalEntrega { get; set; }
        public string RecebimentoDocumentacaoInstalacao { get; set; }
        public string FaturaTranspReEntrega { get; set; }
        public DateTime? DtReEntrega { get; set; }
        public string ResponsavelRecebReEntrega { get; set; }
        public DateTime? DataEntregaConfirmacao { get; set; }
        public string ResponsavelRecebimento { get; set; }
        public string MatResponsavelRecebimento { get; set; }
        public string BorderoTranspRecebido { get; set; }
        public string BorderoTranspConferido { get; set; }
        public string NFRemessaConferida { get; set; }
        public DateTime? PrevisaoInstalacao { get; set; }
        public DateTime? DtAgendadaInstalacao { get; set; }
        public int? OS { get; set; }
        public DateTime? DataHoraOS { get; set; }
        public string StatusOS { get; set; }
        public string RAT { get; set; }
        public string AgInstalacao { get; set; }
        public string NomeLocalInstalacao { get; set; }
        public DateTime? DtInstalacao { get; set; }
        public int? QtdParabold { get; set; }
        public string EquipamentoRebaixado { get; set; }
        public string ResponsavelInstalacaoBanco { get; set; }
        public string MatResponsavelInstalacaoBanco { get; set; }
        public string TermoAceite { get; set; }
        public byte? TermoDescaracterizacao { get; set; }
        public string Laudo { get; set; }
        public string RE5330 { get; set; }
        public string RATEntregue { get; set; }
        public string FornecedorTradeIn1 { get; set; }
        public string FornecedorTradeIn2 { get; set; }
        public string NumBemTradeIn { get; set; }
        public string FornecedorBemTradeIn { get; set; }
        public string TipoBemTradeIn { get; set; }
        public string NFTradeIn1 { get; set; }
        public string NFTradeIn2 { get; set; }
        public DateTime? DataNFTradeIn1 { get; set; }
        public DateTime? DataNFTradeIn2 { get; set; }
        public decimal? ValorTradeIn1 { get; set; }
        public decimal? ValorTradeIn2 { get; set; }
        public decimal? ValorKmTradeIn1 { get; set; }
        public decimal? ValorKmTradeIn2 { get; set; }
        public decimal? DistanciaKmTradeIn1 { get; set; }
        public decimal? DistanciaKmTradeIn2 { get; set; }
        public decimal? ValorTotalTradeIn1 { get; set; }
        public decimal? ValorTotalTradeIn2 { get; set; }
        public string TransportadoraTradeIn { get; set; }
        public DateTime? DtPrevRecolhimentoTradeIn { get; set; }
        public DateTime? DataRetiradaBemTradeIn { get; set; }
        public string Romaneio { get; set; }
        public string NFTranspTradeIn { get; set; }
        public DateTime? DataNFTransTradeIn { get; set; }
        public decimal? ValorRecolhimentoTradeIn { get; set; }
        public string ReponsavelBancoAcompanhamentoTradeIn { get; set; }
        public string MatriculaRespBancoAcompanhamentoRecolhimento { get; set; }
        public string FornecedorCompraTradeIn { get; set; }
        public string NFVendaTradeIn { get; set; }
        public DateTime? DtNFVendaTradeIn { get; set; }
        public decimal? ValorVendaTradeIn { get; set; }
        public string NFInstalacaoAutorizada { get; set; }
        public DateTime? DtNFInstalacaoAutorizada { get; set; }
        public int? NFVenda { get; set; }
        public DateTime? NFVendaDataEmissao { get; set; }
        public DateTime? DtEnvioNFVenda { get; set; }
        public DateTime? DtRecebimentoNFVenda { get; set; }
        public DateTime? DtPagtoEntrega { get; set; }
        public decimal? VlrPagtoEntrega { get; set; }
        public DateTime? DtPagtoInstalacao { get; set; }
        public decimal? VlrPagtoInstalacao { get; set; }
        public int? Bordero { get; set; }
        public DateTime? DTVencimentoBordero100Perc { get; set; }
        public DateTime? DTEntregaBordero100Perc { get; set; }
        public DateTime? DTVencimentoBordero90Perc { get; set; }
        public DateTime? DTEntregaBordero90Perc { get; set; }
        public DateTime? DTVencimentoBordero10Perc { get; set; }
        public DateTime? DTEntregaBordero10Perc { get; set; }
        public decimal? ValorFrete1 { get; set; }
        public string FaturaFrete1 { get; set; }
        public string CteFrete1 { get; set; }
        public DateTime? DataFaturaFrete1 { get; set; }
        public decimal? ValorFrete2 { get; set; }
        public string FaturaFrete2 { get; set; }
        public string CteFrete2 { get; set; }
        public DateTime? DataFaturaFrete2 { get; set; }
        public decimal? ValorExtraFrete { get; set; }
        public string DDD { get; set; }
        public string Telefone1 { get; set; }
        public string Redestinacao { get; set; }
        public string AntigoPrefixoRedestinacao { get; set; }
        public string AntigoSbRedestinacao { get; set; }
        public string AntigoNomeDependenciaRedestinacao { get; set; }
        public string AntigoUfRedestinacao { get; set; }
        public string AntigoTipoDependenciaRedestinacao { get; set; }
        public string AntigoPedidoCompraRedestinacao { get; set; }
        public string AntigoProtocoloCdo { get; set; }
        public string NovoProtocoloCdo { get; set; }

    }
}
