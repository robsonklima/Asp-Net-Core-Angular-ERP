using System;

namespace SAT.MODELS.Entities
{
    public class MbscAgendaTecnicoCalendarEvent
    {
        public int CodAgendaTecnico { get; set; }
        public int Resource { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
        public Boolean Editable { get; set; }
        public OrdemServico OrdemServico { get; set; }
        public AgendaTecnico AgendaTecnico { get; set; }
    }
}