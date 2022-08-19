using System.Collections.Generic;

namespace SAT.MODELS.Entities
{
    public class Email
    {
        public string EmailRemetente { get; set; }
        public string NomeRemetente { get; set; }
        public string NomeCC { get; set; }
        public string EmailCC { get; set; }
        public string EmailDestinatario { get; set; }
        public string NomeDestinatario { get; set; }
        public string Assunto { get; set; }
        public string Descricao { get; set; }
        public string Corpo { get; set; }
        public List<string> Anexos { get; set; }
    }
}