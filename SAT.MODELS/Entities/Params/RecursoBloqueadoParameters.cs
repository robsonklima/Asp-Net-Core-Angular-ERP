using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class RecursoBloqueadoParameters : QueryStringParameters
    {
        public int? CodSetor { get; set; }
        public int? CodPerfil { get; set; }
        public string Claim { get; set; }
        public string Url { get; set; }
    }
}
