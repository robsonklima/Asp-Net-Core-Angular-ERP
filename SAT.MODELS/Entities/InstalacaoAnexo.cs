using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("InstalAnexo") ]
    public class InstalacaoAnexo
    {
        [Key]
        public int CodInstalAnexo { get; set; }
        public int? CodInstalacao { get; set; }
        public int? CodInstalPleito { get; set; }
        public int? CodInstalLote { get; set; }
        public string NomeAnexo { get; set; }
        public string DescAnexo { get; set; }
        public string SourceAnexo { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string Base64 { get; set; }
    }
}