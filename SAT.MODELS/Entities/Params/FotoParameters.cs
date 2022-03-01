using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class FotoParameters : QueryStringParameters
    {
        public string NumRAT { get; set; }
        public int? CodOS { get; set; }

        public string CodUsuario { get; set; }
    }
}