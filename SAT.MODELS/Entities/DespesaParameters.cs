using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class DespesaParameters : QueryStringParameters
    {
        public int? CodDespesaPeriodo { get; set; }
        public int? CodTecnico { get; set; }
        public string CodRATs { get; set; }
    }
}