using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [NotMapped]
    public class Indicador
    {
        public string Label { get; set; }
        public decimal Valor { get; set; }
        public List<Indicador> Filho { get; set; }
    }
}