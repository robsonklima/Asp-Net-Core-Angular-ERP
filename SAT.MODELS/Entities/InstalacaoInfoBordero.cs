using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalacaoInfoBordero
    {
        [Key]
        public int CodInstalacao { get; set; }
        public int? IndStatusProtocolado { get; set; }
        public int? CodStatusDocumentocao { get; set; }
        public int? CodStatusInstalacao { get; set; }
        public decimal? ValorBordero { get; set; }
        public string CodUsuario { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}