using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
public class InstalacaoTipoParcelaParameters: QueryStringParameters
    {
        public int? CodInstalTipoParcela { get; set; }
        public string NomeTipoParcela { get; set; }
        public byte? IndAtivo { get; set; }
    }
}