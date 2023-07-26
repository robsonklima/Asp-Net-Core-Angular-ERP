using System;
using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoPagtoParameters: QueryStringParameters
    {
        public int? CodInstalPagto { get; set; }
        public int? CodContrato { get; set; }
        public int? CodCliente { get; set; }
        public string CodContratos { get; set; }
        public string CodTipoContratos { get; set; }
        public DateTime? DataPagto { get; set; }
        public decimal? VlrPagto { get; set; }
    }
}