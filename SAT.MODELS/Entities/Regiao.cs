using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Regiao
    {
        [Key]
        public int CodRegiao { get; set; }
        public string NomeRegiao { get; set; }
        public byte IndAtivo { get; set; }
    }
}
