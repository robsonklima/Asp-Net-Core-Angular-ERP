using System;

namespace SAT.MODELS.Entities
{
    public class OrcamentoDesconto
    {
        public int CodOrcDesconto { get; set; }
        public int? CodOrc { get; set; }
        public int? IndiceCampo { get; set; }
        public int? IndiceTipo { get; set; }
        public string NomeCampo { get; set; }
        public string NomeTipo { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorTotal { get; set; }
        public string Motivo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string UsuarioCadastro { get; set; }
    }
}