using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class InstalacaoTipoParcela
    {
        public int CodInstalTipoParcela { get; set; }
        public string NomeTipoParcela { get; set; }
        public byte IndAtivo { get; set; }
    }
}