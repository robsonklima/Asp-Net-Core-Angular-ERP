using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class SATFeriadosParameters : QueryStringParameters
    {
        public int? CodSATFeriados { get; set; }
        public string Municipios { get; set; }
        public string Tipos { get; set; }
    }
}
