using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities {
    public class IntegracaoFinanceiroRetorno
    {
        public bool Sucesso { get; set; }
        public string Retorno { get; set; }
        public string MensagemErro { get; set; }
    }

    public class Item
    {
        public string CodItem { get; set; }
        public int Quantidade { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorTotal { get; set; }
        public double ValorDesconto { get; set; }
        public object ValorEnviado { get; set; }
        public object ValorImposto { get; set; }
        public string Status { get; set; }
    }

    public class Retorno
    {
        public string NumeroOrcamento { get; set; }
        public object NumeroPedido { get; set; }
        public object NotaFiscal { get; set; }
        public object Serie { get; set; }
        public string TipoOperacao { get; set; }
        public string TipoServico { get; set; }
        public double ValorTotal { get; set; }
        public object DataEmissao { get; set; }
        public DateTime DataSolicitacao { get; set; }
        public string Status { get; set; }
        public List<Item> Itens { get; set; }
    }

    public class IntegracaoFinanceiroOrcamento
    {
        public bool Sucesso { get; set; }
        public List<Retorno> Retorno { get; set; }
        public string MensagemErro { get; set; }
    }
}