using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Defeito
    {
        [Key]
        public int? CodDefeito { get; set; }
        public string CodEDefeito { get; set; }
        public string NomeDefeito { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
