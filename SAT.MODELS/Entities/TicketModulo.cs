using System;
using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities 
{
    public class TicketModulo
    {
        [Key]
        public int CodModulo { get; set; }
        public string Descricao { get; set; }
    }
}