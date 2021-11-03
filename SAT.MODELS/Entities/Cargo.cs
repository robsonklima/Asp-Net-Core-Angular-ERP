using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Cargo
    {
        [Key]
        public int CodCargo { get; set; }
        public string NomeCargo { get; set; }
        public byte IndAtivo { get; set; }
    }
}
