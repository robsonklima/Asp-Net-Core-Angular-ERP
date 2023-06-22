using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
public class SatTaskParameters : QueryStringParameters
    {
        public int? CodSatTaskTipo { get; set; }
        public byte? IndProcessado { get; set; }
    }
}
