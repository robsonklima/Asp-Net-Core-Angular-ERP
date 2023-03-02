using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoPleitoParameters: QueryStringParameters
    {
        public int? CodInstalPleito { get; set; }
        public int? CodContrato { get; set; }
        public int? CodInstalTipoPleito { get; set; }
        public string CodContratos { get; set; }
        public string CodInstalTipoPleitos { get; set; }                
        public string CodClientes { get; set; }
    }
}