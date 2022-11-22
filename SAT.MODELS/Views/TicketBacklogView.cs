using System;

namespace SAT.MODELS.Entities {
    public class TicketBacklogView
    {
        public DateTime Data { get; set; }        
        public int Abertos { get; set; }        
        public int Fechados { get; set; }        
        public int Backlog { get; set; }        
    }
}