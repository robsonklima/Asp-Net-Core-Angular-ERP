using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class Perfil
    {
        [Key]
        public int CodPerfil { get; set; }
        public string NomePerfil { get; set; }
        public string DescPerfil { get; set; }
        public byte? IndResumo { get; set; }
        public byte? IndAbreChamado { get; set; }
        public virtual ICollection<NavegacaoConfiguracao> NavegacoesConfiguracao { get; set; }
    }
}
