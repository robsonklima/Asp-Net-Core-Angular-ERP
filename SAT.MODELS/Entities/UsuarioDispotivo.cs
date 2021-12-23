using System;

namespace SAT.MODELS.Entities
{
    public class UsuarioDispositivo
    {
        public int CodUsuarioDispositivo { get; set; }
        public string CodUsuario { get; set; }
        public string Hash { get; set; }
        public DateTime DataHoraCad { get; set; }
        public byte IndAtivo { get; set; }
    }
}