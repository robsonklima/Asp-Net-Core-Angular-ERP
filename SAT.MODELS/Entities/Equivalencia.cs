using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Equivalencia
    {
        public int CodEquivalencia { get; set; }
        [Key]
        public int CodEquip { get; set; }
        public string Regra { get; set; }
        public int Multiplicador { get; set; }
        public double ValorCalculado { get; set; }
    }
}
