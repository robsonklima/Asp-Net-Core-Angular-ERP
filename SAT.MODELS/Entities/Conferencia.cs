using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class Conferencia
    {
        public int CodConferencia { get; set; }
        public string Nome { get; set; }
        public string Sala { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCadastro { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public Usuario UsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public List<ConferenciaParticipante> Participantes { get; set; }
        public byte IndAtivo { get; set; }
    }
}