using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class OrcamentoParameters : QueryStringParameters
    {
        public int? codOrc { get; set; }
        public string codStatusServicos { get; set; }
        public string codFiliais { get; set; }
        public string codClientes { get; set; }
        public string CodTiposIntervencao { get; set; }
        public int? CodigoOrdemServico { get; set; }
        public string NumOSCliente { get; set; }
        public string NumSerie { get; set; }
        public string Numero { get; set; }
        public bool IsFaturamento { get; set; }
        public bool IncluirLaudoExportacao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime? DataAberturaInicio { get; set; }
        public DateTime? DataAberturaFim { get; set; }   
    }
}