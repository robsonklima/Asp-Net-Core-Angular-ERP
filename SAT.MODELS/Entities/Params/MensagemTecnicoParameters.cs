using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class MensagemTecnicoParameters : QueryStringParameters
    {
        public byte? IndAtivo { get; set; }
        public string Mensagem { get; set; }
        public string Assunto { get; set; }
    }
}
