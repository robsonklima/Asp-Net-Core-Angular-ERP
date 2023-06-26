using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
public class SatTaskProcessoParameters : QueryStringParameters
    {
        public int? CodSatTaskProcesso { get; set; }        
        public int? CodSatTaskTipo { get; set; }
        public int? CodOS { get; set; }       
        public byte? IndProcessado { get; set; } 
    }
}
