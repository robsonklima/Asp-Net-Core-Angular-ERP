using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class InstalacaoRessalvaParameters: QueryStringParameters
    {
        public int? CodInstalRessalva { get; set; }
        public int? CodInstalacao { get; set; }
    }
}