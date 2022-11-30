using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoPleitoParameters: QueryStringParameters
    {
        public int? CodContrato { get; set; }
        public int? CodInstalPleito { get; set; }
        public int? CodInstalTipoPleito { get; set; }
    }
}