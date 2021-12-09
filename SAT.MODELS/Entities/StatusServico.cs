using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class StatusServico
    {
        [Key]
        public int CodStatusServico { get; set; }
        public string NomeStatusServico { get; set; }
        public byte? IndPendente { get; set; }
        public byte? IndEncerrado { get; set; }
        public string CorFundo { get; set; }
        public string CorFonte { get; set; }
        public byte? TamFonte { get; set; }
        public byte? IndNegrito { get; set; }
        public int? CodTraducao { get; set; }
        public string Abrev { get; set; }
        public byte? IndServico { get; set; }
        public byte IndAtivo { get; set; }
        [Column("IndLiberadoOSBloqueado")]
        public byte? IndLiberadoOsbloqueado { get; set; }
    }
}
