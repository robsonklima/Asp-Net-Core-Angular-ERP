using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class DespesaItemAlerta
    {
        [Key]
        public int CodDespesaItemAlerta { get; set; }
        public string DescItemAlerta { get; set; }
    }
}