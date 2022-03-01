using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class TipoIndiceReajusteParameters : QueryStringParameters
    {
        public int? CodTipoIndiceReajuste { get; set; }
        public string NomeTipoIndiceReajuste { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
