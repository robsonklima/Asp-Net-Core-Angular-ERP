using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class Navegacao
    {
        [Key]
        public int? CodNavegacao { get; set; }
        public int? CodNavegacaoPai { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }
        [ForeignKey("CodNavegacaoPai")]
        public virtual ICollection<Navegacao> Children { get; set; }
        public int Ordem { get; set; }
        public byte? IndAtivo { get; set; }
        [NotMapped]
        public string Id { get; set; }
        public virtual ICollection<NavegacaoConfiguracao> NavegacoesConfiguracao { get; set; }
    }
}
