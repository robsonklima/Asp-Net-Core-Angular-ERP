namespace SAT.MODELS.Entities {
    public class EmailConfiguration
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int? Port { get; set; }
        public string ClientID { get; set; }
        public string ClientSecret { get; set; }
        public string Tenant { get; set; }
        public string ApiUri { get; set; }
        public string Instance { get; set; }
    }
}