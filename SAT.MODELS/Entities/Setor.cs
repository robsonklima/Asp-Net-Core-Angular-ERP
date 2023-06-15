using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Setor
    {
        public int CodSetor { get; set; }
        public string NomeSetor { get; set; }
        public string Abreviacao { get; set; }
        public int? IndAtivo { get; set; }

    }
}
