using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Filial
    {
        [Key]
        public int CodFilial { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFilial { get; set; }
        [ForeignKey("CodCidade")]
        public Cidade Cidade { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
