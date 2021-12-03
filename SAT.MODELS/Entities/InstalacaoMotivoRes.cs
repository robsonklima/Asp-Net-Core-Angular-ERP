using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("InstalMotivoRes")]
    public class InstalacaoMotivoRes
    {
        [Key]
        public int CodInstalMotivoRes { get; set; }
        public string DescMotivoRes { get; set; }
        public string SiglaMotivoRes { get; set; }
        public byte IndTipoRes { get; set; }
        public byte IndAtivo { get; set; }
    }
}