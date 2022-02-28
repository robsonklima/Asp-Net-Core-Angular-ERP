using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class AcordoNivelServicoParameters : QueryStringParameters
    {
        public int? CodSLA { get; set; }
        public string NomeSLA { get; set; }
    }
}
