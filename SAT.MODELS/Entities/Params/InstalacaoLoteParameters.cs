using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoLoteParameters: QueryStringParameters
    {
        public int? CodContrato { get; set; }
    }
}