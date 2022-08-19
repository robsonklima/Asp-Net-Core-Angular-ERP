using System;
using SAT.MODELS.Enums;

namespace SAT.MODELS.ViewModels{
    public class OrcamentoFaturamentoViewModel
    {
        public int? Codigo { get; set; }
        public string Cliente { get; set; }
        public string Filial { get; set; }
        public int? CodOS { get; set; }
        public string NumOSCliente { get; set; }
        public string NumOrcamento { get; set; }
        public string DescNF { get; set; }
        public double? NumNF { get; set; }        
        public string DataEmissao { get; set; }
        public double? IndFaturado { get; set; }
        public int? CodOrc { get; set; }
        public OrcamentoFaturamentoTipoEnum Tipo { get; set; }
        public int? CodFilial { get; set; }
        public string CodClienteBancada { get; set; }
        public string ValorPeca { get; set; }
        public double? QtdePeca { get; set; }
        public string ValorServico { get; set; }
        public string IndRegistroDanfe { get; set; }
        public string CaminhoDanfe { get; set; }
    }
}