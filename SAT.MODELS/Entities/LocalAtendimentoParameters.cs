using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities
{
    public class LocalAtendimentoParameters : QueryStringParameters
    {
        public int? CodPosto { get; set; }
        public int? CodCliente { get; set; }
        public int? IndAtivo { get; set; }
        public string NumAgencia { get; set; }
        public string DCPosto{ get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodRegiao { get; set; }
    }
}
