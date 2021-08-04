using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Acao
    {
        [Key]
        public int CodAcao { get; set; }
        public string CodEAcao { get; set; }
        public string NomeAcao { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
