using System;

namespace SAT.MODELS.Entities
{
    public class Conferencia
    {
        public int CodConferencia { get; set; }
        public string Link { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public Usuario UsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public byte IndAtivo { get; set; }
    }
}