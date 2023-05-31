using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("TecnicoDeslocamento")]
    [Index(nameof(CodDespesaItem), Name = "UC_TecnicoDeslocamento_1", IsUnique = true)]
    public partial class TecnicoDeslocamento
    {
        [Key]
        public int CodTecnicoDeslocamento { get; set; }
        public int CodDespesaItem { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Required]
        [StringLength(200)]
        public string EnderecoOrigem { get; set; }
        [StringLength(10)]
        public string NumOrigem { get; set; }
        [Required]
        [StringLength(150)]
        public string NomeCidadeOrigem { get; set; }
        [Required]
        [Column("UFOrigem")]
        [StringLength(2)]
        public string Uforigem { get; set; }
        public double? LatitudeOrigem { get; set; }
        public double? LongitudeOrigem { get; set; }
        [Required]
        [StringLength(200)]
        public string EnderecoDestino { get; set; }
        [StringLength(10)]
        public string NumDestino { get; set; }
        [Required]
        [StringLength(150)]
        public string NomeCidadeDestino { get; set; }
        [Required]
        [Column("UFDestino")]
        [StringLength(2)]
        public string Ufdestino { get; set; }
        public double? LatitudeDestino { get; set; }
        public double? LongitudeDestino { get; set; }
        public double? DistanciaEmQuilometros { get; set; }
        public double? TempoEmSegundos { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
