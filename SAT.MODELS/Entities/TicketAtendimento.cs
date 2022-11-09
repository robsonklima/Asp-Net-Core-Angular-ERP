using System;

namespace SAT.MODELS.Entities {
    public class TicketAtendimento
    {
        public int CodTicketAtend { get; set; }
        public int CodTicket { get; set; }
        public string Descricao { get; set; }
        public int CodStatus { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public Usuario UsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
    }
}