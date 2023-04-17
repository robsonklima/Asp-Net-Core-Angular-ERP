using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoStatusParameters: QueryStringParameters
    {
        public int? CodInstalStatus { get; set; }
        public string NomeInstalStatus { get; set; }
    }
}