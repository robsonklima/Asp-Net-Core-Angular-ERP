using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("InstalGrupoAcesso") ]
    public class InstalacaoGrupoAcesso
    {
        [Key]
        public int CodGrupoAcesso { get; set; }
        public string Codigo { get; set; }
        public int IndTipoAcesso { get; set; }
        public string IndAcessoTodos { get; set; }
        public string IndAcessoImplantacao { get; set; }
        public string IndAcessoTradeIn { get; set; }
        public string IndAcessoFinanceiro { get; set; }
    }
}