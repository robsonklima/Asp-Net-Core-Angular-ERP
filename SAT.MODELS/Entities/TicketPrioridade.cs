using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities 
{
    public class TicketPrioridade
    {
        [Key]
        public int CodPrioridade { get; set; }
        public string Descricao { get; set; }
    }
}