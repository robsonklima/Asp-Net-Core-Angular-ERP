using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("OR")]
    public partial class Or
    {
        [Column("CodOR")]
        public int CodOr { get; set; }
        [Column("DataHoraOR", TypeName = "datetime")]
        public DateTime DataHoraOr { get; set; }
        public int? CodOrigem { get; set; }
        public int? CodDestino { get; set; }
        [Column("CodStatusOR")]
        public int CodStatusOr { get; set; }
        [Column("NumNF")]
        [StringLength(20)]
        public string NumNf { get; set; }
        [StringLength(20)]
        public string Volumes { get; set; }
        public int? CodModal { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataExpedicao { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(200)]
        public string Minuta { get; set; }
        public int? CodTransportadora { get; set; }
    }
}
