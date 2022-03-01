using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ContratoReajusteParameters : QueryStringParameters
    {       
        public int? CodContratoReajuste { get; set; }
        public int? CodContrato { get; set; }
        public int? CodTipoIndiceReajuste { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
