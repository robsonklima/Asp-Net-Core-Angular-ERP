using System;

namespace SAT.MODELS.Entities
{
    public class OrcamentoMaoDeObra
    {
        public int CodOrcMaoObra { get; set; }
        public int? CodOrc { get; set; }
        public int? PrevisaoHoras { get; set; }
        public decimal? ValorHoraTecnica { get; set; }
        public decimal? ValorTotal { get; set; }
        public decimal? Redutor { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string UsuarioCadastro { get; set; }
    }
}