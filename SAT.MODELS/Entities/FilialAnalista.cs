using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public partial class FilialAnalista
    {
        [Key]
        public int CodFilialAnalista { get; set; }
        public int CodFilial { get; set; }
        public string CodUsuario { get; set; }
        public string NomeUsuario { get; set; }
    }
}
