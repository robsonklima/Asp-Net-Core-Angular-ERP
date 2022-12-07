using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class LaudoSituacaoParameters : QueryStringParameters
    {
        public int? CodLaudoSituacao { get; set; }
        public int? CodLaudo { get; set; }
        
    }
}