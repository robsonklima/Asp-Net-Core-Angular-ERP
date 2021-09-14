using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class PecaParameters : QueryStringParameters
    {
        public int? CodPeca { get; set; }
        public string CodMagnus { get; set; }
    }
}
