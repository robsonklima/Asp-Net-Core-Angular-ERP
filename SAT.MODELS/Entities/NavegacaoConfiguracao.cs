using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("NavegacaoConf")]
    public class NavegacaoConfiguracao
    {
        [Key]
        public int CodNavegacaoConfiguracao { get; set; }
        public int CodNavegacao { get; set; }
        public int CodPerfil { get; set; }
        [ForeignKey("CodNavegacao")]
        public Navegacao Navegacao { get; set; }
        [ForeignKey("CodPerfil")]
        public Perfil Perfil { get; set; }
    }
}
