using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class InstalacaoPagto
    {
        public int CodInstalPagto { get; set; }
        public int? CodContrato { get; set; }
        public DateTime DataPagto { get; set; }
        public decimal VlrPagto { get; set; }
        public string ObsPagto { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public Contrato Contrato { get; set; }
        public List<InstalacaoPagtoInstal> InstalacoesPagtoInstal { get; set; }        
    }
}