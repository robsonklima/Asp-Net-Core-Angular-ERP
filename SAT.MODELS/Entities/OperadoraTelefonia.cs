using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class OperadoraTelefonia
    {
        [Key]
        public int CodOperadoraTelefonia { get; set; }
        public string NomeOperadoraTelefonia { get; set; }
        public bool IndAtivo { get; set; }
    }
}