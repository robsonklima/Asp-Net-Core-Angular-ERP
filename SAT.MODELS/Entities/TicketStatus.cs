using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities 
{
    public class TicketStatus
    {
        [Key]
        public int CodStatus { get; set; }
        public string Descricao { get; set; }
    }
}