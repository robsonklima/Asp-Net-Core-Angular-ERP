namespace SAT.MODELS.Entities {
    public class Email {
        public string EmailRemetente { get; set; }
        public string NomeRemetente { get; set; }
        public string EmailDestinatario { get; set; }
        public string NomeDestinatario { get; set; }
        public string Assunto { get; set; }
        public string Corpo { get; set; }
    }
}