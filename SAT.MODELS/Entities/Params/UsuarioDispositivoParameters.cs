using SAT.MODELS.Entities.Helpers;

namespace SAT.MODELS.Entities.Params
{
    public class UsuarioDispositivoParameters : QueryStringParameters
    {
        public string CodUsuario { get; set; }
        public string SistemaOperacional { get; set; }
        public string VersaoSO { get; set; }
        public string Navegador { get; set; }
        public string VersaoNavegador { get; set; }
        public string TipoDispositivo { get; set; }
        public string Ip { get; set; }
        public byte? IndAtivo { get; set; }
    }
}
