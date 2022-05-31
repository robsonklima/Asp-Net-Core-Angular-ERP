using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class InstalacaoNFVendaParameters: QueryStringParameters
    {
        public int? CodInstalNFVenda { get; set; }
        public string CodClientes { get; set; }
        public int? NumNFVenda { get; set; }
    }
}