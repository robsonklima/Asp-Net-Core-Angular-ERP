using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class AcordoNivelServicoParameters : QueryStringParameters
    {
        public int? CodSLA { get; set; }
        public string NomeSLA { get; set; }
    }
}
