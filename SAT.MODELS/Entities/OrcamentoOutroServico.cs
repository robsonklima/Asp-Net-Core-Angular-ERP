using System;

namespace SAT.MODELS.Entities
{
    public class OrcamentoOutroServico
    {
        public int CodOrcOutroServico { get; set; }
        public int? CodOrc { get; set; }
        public string Tipo { get; set; }
        public string Descricao { get; set; }
        public decimal? ValorUnitario { get; set; }
        public int? Quantidade { get; set; }
        public decimal? ValorTotal { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string UsuarioCadastro { get; set; }
    }
}