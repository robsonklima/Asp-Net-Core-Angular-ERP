using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalPagto
    {
        [Key]
        public int CodInstalPagto { get; set; }
        public int CodContrato { get; set; }
        public DateTime DataPagto { get; set; }
        public decimal VlrPagto { get; set; }
        public string ObsPagto { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}