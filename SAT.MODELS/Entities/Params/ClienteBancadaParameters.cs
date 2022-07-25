using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class ClienteBancadaParameters : QueryStringParameters
    {
        public int? IndAtivo { get; set; }
        public string CodCidades { get; set; }
        public string CodClienteBancadas { get; set; }

    
    }
}
