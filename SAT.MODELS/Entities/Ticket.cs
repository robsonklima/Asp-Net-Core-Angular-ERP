using System;
using System.Collections.Generic;

namespace SAT.MODELS.Entities {
    public class Ticket
    {
        public int CodTicket { get; set; }
        public int CodModulo { get; set; }
        public TicketModulo TicketModulo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int CodPrioridade { get; set; }
        public TicketPrioridade TicketPrioridade  { get; set; }
        public int CodClassificacao { get; set; }
        public TicketClassificacao TicketClassificacao { get; set; }
        public int CodStatus { get; set; }
        public TicketStatus TicketStatus { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string CodUsuarioManut { get; set; }
        public Usuario UsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public DateTime? DataHoraFechamento { get; set; }
        public int? Ordem { get; set; }
        public byte? IndAtivo { get; set; }
        public List<TicketAtendimento> Atendimentos { get; set; }
    }
}