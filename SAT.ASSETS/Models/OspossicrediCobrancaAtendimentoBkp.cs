using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("OSPOSSicrediCobrancaAtendimento_BKP")]
    public partial class OspossicrediCobrancaAtendimentoBkp
    {
        public int Id { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        public int Distancia { get; set; }
        public int Tempo { get; set; }
        [Column("KMPadrao")]
        public int Kmpadrao { get; set; }
        public int HoraPadrao { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorPorKm { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorPorHora { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal ValorPorServico { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorCobrarHoraExcedente { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ValorCobrarKmExcedente { get; set; }
        public int? HoraExcedente { get; set; }
        public int? KmExcedente { get; set; }
        [Required]
        [StringLength(8000)]
        public string EnderecoOrigem { get; set; }
        [Required]
        [StringLength(8000)]
        public string EnderecoDestino { get; set; }
        [Required]
        [StringLength(8000)]
        public string EnderecoOrigemGoogle { get; set; }
        [Required]
        [StringLength(8000)]
        public string EnderecoDestinoGoogle { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataManutencao { get; set; }
        public int CodTipoIntervencao { get; set; }
        public bool? VisitaImprodutiva { get; set; }
    }
}
