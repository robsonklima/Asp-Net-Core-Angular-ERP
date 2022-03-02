using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class CargoParameters : QueryStringParameters
    {
        public int? CodCargo { get; set; }
        public int? IndAtivo { get; set; }
        public int? CodUF { get; set; }
    }
}