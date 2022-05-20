using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class IntegracaoCobraParameters: QueryStringParameters
    {
        public int? CodOS { get; set; }
        public string NumOscliente { get; set; }
        public string NomeTipoArquivoEnviado { get; set; }
    }
}