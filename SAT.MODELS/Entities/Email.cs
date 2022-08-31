using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class Email
    {
        public string Assunto { get; set; }
        public string Corpo { get; set; }
        public string[] EmailDestinatarios { get; set; }
        public List<string> Anexos { get; set; }
    }
}