using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoParameters: QueryStringParameters
    {
        public int? CodContrato { get; set; }
        public int? CodInstalLote { get; set; }
        public int? CodInstalacao { get; set; }
        public string CodInstalacoes { get; set; }
    }
}