using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoPleitoInstalParameters: QueryStringParameters
    {
        public int? CodInstalacao { get; set; }
        public int? CodInstalPleito { get; set; }
        public int? CodEquipContrato { get; set; }
    }
}