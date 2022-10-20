using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class MensagemParameters : QueryStringParameters
    {
        public string CodUsuarioDestinatario { get; set; }
        public byte? IndLeitura { get; set; }
    }
}
