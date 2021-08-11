using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Causa
    {
        [Key]
        public int CodCausa { get; set; }
        public int CodTipoCausa { get; set; }
        public int CodGrupoCausa { get; set; }
        public string CodECausa { get; set; }
        public string NomeCausa { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }
    }
}
