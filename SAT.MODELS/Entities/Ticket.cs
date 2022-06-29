using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities {
    public class Ticket
    {
        public int CodTicket { get; set; }
        public int CodModulo { get; set; }
        [ForeignKey("CodModulo")]
        public TicketModulo TicketModulo { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public int CodPrioridade { get; set; }
         [ForeignKey("CodPrioridade")]
        public TicketPrioridade TicketPrioridade  { get; set; }
        public int CodClassificacao { get; set; }
        [ForeignKey("CodClassificacao")]
        public TicketClassificacao TicketClassificacao { get; set; }
        public int CodStatus { get; set; }
        [ForeignKey("CodStatus")]
        public TicketStatus TicketStatus { get; set; }
        public string CodUsuario { get; set; }
        [ForeignKey("CodUsuario")]
        public Usuario Usuario { get; set; }
        public DateTime DataCadastro { get; set; }
        public string UsuarioManut { get; set; }
        public DateTime? DataManut { get; set; }
        public DateTime? DataFechamento { get; set; }
    }
}