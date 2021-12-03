using System.ComponentModel.DataAnnotations;

namespace SAT.MODELS.Entities
{
    public class TipoServico
    {
        [Key]
        public int CodServico { get; set; }
        public string NomeServico { get; set; }
        public string CodETipoServico { get; set; }
        public decimal? ValServico { get; set; }
        public byte? IndValHora { get; set; }
        public decimal? ValPrimHora { get; set; }
        public decimal? ValSegHora { get; set; }
        public byte? IndAtivo { get; set; }
        public int? CodTraducao { get; set; }
    }
}
