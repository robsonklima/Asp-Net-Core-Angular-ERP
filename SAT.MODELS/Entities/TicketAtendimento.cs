using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities {
    public class TicketAtendimento
    {
        [Key]
        public int CodTicketAtend { get; set; }
        public int CodTicket { get; set; }
        public string Descricao { get; set; }
        public int CodStatus { get; set; }
        [ForeignKey("CodStatus")]
        public TicketStatus TicketStatus { get; set; }
        public string UsuarioAtend { get; set; }
        [ForeignKey("UsuarioAtend")]
        public Usuario Usuario { get; set; }
        public DateTime DataCadastro { get; set; }
    }
}