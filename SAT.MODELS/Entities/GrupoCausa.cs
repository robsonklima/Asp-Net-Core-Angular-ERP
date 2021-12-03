using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class GrupoCausa
    {
        public int CodTipoCausa { get; set; }
        [Key]
        public int CodGrupoCausa { get; set; }
        public string CodEGrupoCausa { get; set; }
        public string NomeGrupoCausa { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }
    }
}
