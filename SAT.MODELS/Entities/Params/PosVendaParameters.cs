using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class PosVendaParameters : QueryStringParameters
    {
        public int? CodPosvenda { get; set; }
        public string Nome { get; set; }
        public int? CodigoLogix { get; set; }
    }
}
