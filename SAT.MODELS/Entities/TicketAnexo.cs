using System;

namespace SAT.MODELS.Entities {
    public class TicketAnexo
    {
        public int CodTicketAnexo { get; set; }
        public int CodTicket { get; set; }
        public string Url { get; set; }
        public byte IndAtivo { get; set; }
        public string CodUsuarioCad { get; set; }
        public Usuario UsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
        public string Base64 { get; set; }
    }
}