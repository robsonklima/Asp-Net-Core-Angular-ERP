using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class SATFeriadoParameters : QueryStringParameters
    {
        public string Municipio { get; set; }
        public string Tipo { get; set; }
        public string UF { get; set; }
        public int? Mes { get; set; }
    }
}
