using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class FaturamentoFinanceiro
    {
        public string TipoFaturamento { get; set; }
        public string TipoServico { get; set; }
        public string CodEmpresaFaturamento { get; set; }
        public string ClienteCNPJCPF { get; set; }
        public string ClienteRazaoSocial { get; set; }
        public string ClienteNomeFantasia { get; set; }
        public string TipoPessoa { get; set; }
        public string ClienteEndereco { get; set; }
        public string ClienteComplemento { get; set; }
        public string ClienteBairro { get; set; }
        public string ClienteCidade { get; set; }
        public string ClienteUF { get; set; }
        public string ClienteCEP { get; set; }
        public string CodCondicaoPagamento { get; set; }
        public string CodFormaPagamento { get; set; }
        public string CodTipoEntrega { get; set; }
        public string CodTipoFrete { get; set; }
        public string CodModalidadeFrete { get; set; }
        public string CodRepresentante { get; set; }
        public string CodPedidoRepresentante { get; set; }
        public string NumeroDocumento { get; set; }
        public string NumeroOS { get; set; }
        public object PedidoCompra { get; set; }
        public object ObservacaoPedidoCompra { get; set; }
        public object NomeBanco { get; set; }
        public object Agencia { get; set; }
        public object Conta { get; set; }
        public string TextoNota { get; set; }
        public string Usuario { get; set; }
        public List<FaturamentoFinanceiroItem> Itens { get; set; }
    }
}