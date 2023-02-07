using System;

namespace SAT.MODELS.Entities
{
    public class OSBancada
    {
        public int CodOsbancada { get; set; }
        public int CodClienteBancada { get; set; }
        public string Nfentrada { get; set; }
        public DateTime? DataChegada { get; set; }
        public DateTime? DataNf { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataManut { get; set; }
        public string Observacao { get; set; }
        public int? CodFilial { get; set; }
        public int? PrazoEntrega { get; set; }
        public decimal? ValorNf { get; set; }
        public ClienteBancada ClienteBancada { get; set; }
        public Filial Filial { get; set; }
    }
}
