using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class OSBancadaParameters : QueryStringParameters
    {
        public string CodFiliais { get; set; }
        public string CodClienteBancadas { get; set; }

    }
}
