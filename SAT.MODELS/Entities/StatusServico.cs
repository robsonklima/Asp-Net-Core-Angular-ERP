using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class StatusServico
    {
        [Key]
        public int CodStatusServico { get; set; }
        public string NomeStatusServico { get; set; }
        public int? IndPendente { get; set; }
        public int? IndEncerrado { get; set; }
        public string CorFundo { get; set; }
        public string CorFonte { get; set; }
        public int? TamFonte { get; set; }
        public int? IndNegrito { get; set; }
        public string Abrev { get; set; }
        public int? IndServico { get; set; }
        public int IndAtivo { get; set; }
        [Column("IndLiberadoOSBloqueado")]
        public int? IndLiberadoOsbloqueado { get; set; }
    }
}
