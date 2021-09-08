using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnicoParameters : QueryStringParameters
    {
        public int? PA { get; set; }
        public int? CodFilial { get; set; }
        public int? CodTecnico { get; set; }
    }
}
