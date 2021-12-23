using System;

namespace SAT.MODELS.Entities
{
    public class UsuarioDispositivo
    {
        public int CodUsuarioDispositivo { get; set; }
        public string CodUsuario { get; set; }
        public string Hash { get; set; }
        public string SistemaOperacional { get; set; }
        public string VersaoSO { get; set; }
        public string Navegador { get; set; }
        public string VersaoNavegador { get; set; }
        public string TipoDispositivo { get; set; }
        public DateTime DataHoraCad { get; set; }
        public byte IndAtivo { get; set; }
    }
}