using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class FotoParameters : QueryStringParameters
    {
        public int? CodOS { get; set; }
        public string NumRAT { get; set; }
        public string CodUsuario { get; set; }
        public string Modalidade { get; set; }
    }
}