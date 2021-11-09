using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities{
    public class InstalacaoImportacaoLog
    {
        [Key]
        public int CodInstalacaoImportacaoLog { get; set; }
        public string NumSerie { get; set; }
        public string Msg { get; set; }
        public DateTime? DataHoraCadastro { get; set; }
    }
}