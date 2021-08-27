using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS
{
    [NotMapped]
    public class Indicador
    {
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public IEnumerable<Indicador> Filho { get; set; }
    }
}