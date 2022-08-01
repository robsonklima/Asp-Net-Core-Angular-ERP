using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class LiderTecnicoParameters : QueryStringParameters
    {
        public int? IndAtivo { get; set; }

        public string CodUsuarioLideres { get; set; }

        public string CodTecicos { get; set; }
    }
}
