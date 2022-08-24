namespace SAT.MODELS.Entities {
    public class Office365Config
    {
        public string Host { get; set; }
        public string ClientID { get; set; }
        public int? Port { get; set; }
        public string ClientSecret { get; set; }
        public string Instance { get; set; }
        public string Tenant { get; set; }
        public string ApiUri { get; set; }
    }
}