using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalNFAut
    {
        [Key]
        public int CodInstalNfaut { get; set; }
        public int CodAutorizada { get; set; }
        public int CodFilial { get; set; }
        public string NFAut { get; set; }
        public DateTime DataNFAut { get; set; }
        public decimal VlrNFAut { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}