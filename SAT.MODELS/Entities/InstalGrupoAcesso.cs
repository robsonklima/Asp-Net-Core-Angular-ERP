using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class InstalGrupoAcesso
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