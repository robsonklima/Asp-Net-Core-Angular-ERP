using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoAnexoParameters : QueryStringParameters
    {
        public int? CodInstalAnexo { get; set; }
        public int? CodInstalacao { get; set; }
        public int? CodInstalPleito { get; set; }
        public int? CodInstalLote { get; set; }
        public string NomeAnexo { get; set; }
        public string DescAnexo { get; set; }
    }
}