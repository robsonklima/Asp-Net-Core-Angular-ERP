using System;

namespace SAT.MODELS.Entities
{
    public class BancadaLaboratorio
    {
        public string CodUsuario { get; set; }
        public int? NumBancada { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public DateTime? DataHoraCad { get; set; }
    }
}