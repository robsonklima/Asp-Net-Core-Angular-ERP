using System;

namespace SAT.MODELS.Entities
{
    public class ConferenciaParticipante
    {
        public int CodConferenciaParticipante { get; set; }
        public int CodConferencia { get; set; }
        public string CodUsuarioParticipante { get; set; }
        public Usuario UsuarioParticipante { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public DateTime? DataHoraCad { get; set; }
    }
}