using System;

namespace SAT.MODELS.Entities
{
    public class UsuarioLogin
    {
        public int CodUsuarioLogin { get; set; }
        public string CodUsuario { get; set; }
        public string Servidor { get; set; }
        public DateTime DataHoraCad { get; set; }
    }
}