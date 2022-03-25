using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class FerramentaTecnico
    {
        public int CodFerramentaTecnico { get; set; }
        public string Nome { get; set; }
        public int? Status { get; set; }
    }
}
