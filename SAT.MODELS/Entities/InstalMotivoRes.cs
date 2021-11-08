using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalMotivoRes
    {
        [Key]
        public int CodInstalMotivoRes { get; set; }
        public string DescMotivoRes { get; set; }
        public string SiglaMotivoRes { get; set; }
        public byte IndTipoRes { get; set; }
        public byte IndAtivo { get; set; }
    }
}