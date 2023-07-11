using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class RecursoBloqueadoParameters : QueryStringParameters
    {
        public int? CodSetor { get; set; }
        public int? CodPerfil { get; set; }
        public string Claims { get; set; }
        public string Url { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
