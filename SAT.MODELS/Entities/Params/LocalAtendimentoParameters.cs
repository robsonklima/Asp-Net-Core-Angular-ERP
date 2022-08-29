using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class LocalAtendimentoParameters : QueryStringParameters
    {
        public int? CodPosto { get; set; }
        public int? CodCliente { get; set; }
        public int? IndAtivo { get; set; }
        public string NumAgencia { get; set; }
        public string CodFiliais { get; set; }
        public string CodClientes { get; set; }
        public string CodRegioes { get; set; }
        public string DCPosto { get; set; }
        public string DCPostoNotIn { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodFilial { get; set; }
    }
}
