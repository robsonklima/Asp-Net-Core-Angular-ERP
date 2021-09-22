using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class IntegracaoArquivo
    {
        [Key]
        public int CodIntegracaoArquivo { get; set; }
        public int CodIntegracao { get; set; }
        public string TextoRequisicao { get; set; }
        public string Arquivo { get; set; }
        public string Classe { get; set; }
    }
}
