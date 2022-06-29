using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities 
{
    public class TicketClassificacao
    {
        [Key]
        public int CodClassificacao { get; set; }
        public string Descricao { get; set; }
    }
}