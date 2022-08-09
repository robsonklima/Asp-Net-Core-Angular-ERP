using System;

namespace SAT.MODELS.ViewModels {
    public class ViewIntegracaoFinanceiroOrcamento
    {
        public int CodOrc { get; set; }
        public DateTime DataHoraFechamento { get; set; }
        public int CodTipoIntervencao { get; set; }
        public int CodStatusServico { get; set; }
        public int TipoFaturamento { get; set; }
        public int TipoServico { get; set; }
        public int CodEmpresaFaturamento { get; set; }
        public string ClienteCNPJCPF { get; set; }
        public string ClienteRazaoSocial { get; set; }
        public string ClienteNomeFantasia { get; set; }
        public int TipoPessoa { get; set; }
        public string ClienteEndereco { get; set; }
        public string ClienteComplemento { get; set; }
        public string ClienteBairro { get; set; }
        public string ClienteCidade { get; set; }
        public string ClienteUF { get; set; }
        public string ClienteCEP { get; set; }
        public int CodCondicaoPagamento { get; set; }
        public int CodFormaPagamento { get; set; }
        public string CodTipoEntrega { get; set; }
        public string CodTipoFrete { get; set; }
        public string CodModalidadeFrete { get; set; }
        public int CodRepresentante { get; set; }
        public string CodPedidoRepresentante { get; set; }
        public string NumeroDocumento { get; set; }
        public int NumeroOS { get; set; }
        public string PedidoCompra { get; set; }
        public string ObservacaoPedidoCompra { get; set; }
        public string NomeBanco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string TextoNota { get; set; }
    }
}