using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities {
    public class PontoPeriodoStatus
    {
        [Key]
        public int CodPontoPeriodoStatus { get; set; }
        public string NomePeriodoStatus { get; set; }
        public byte IndAtivo { get; set; }
    }
}