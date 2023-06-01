using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DispBBCriticidade")]
    public partial class DispBbcriticidade
    {
        [Column("CodDispBBCriticidade")]
        public int CodDispBbcriticidade { get; set; }
        [Required]
        [StringLength(60)]
        public string Detalhes { get; set; }
        public TimeSpan? HorarioInicio { get; set; }
        public TimeSpan? HorarioFim { get; set; }
        public bool? IndSeg { get; set; }
        public bool? IndTer { get; set; }
        public bool? IndQua { get; set; }
        public bool? IndQui { get; set; }
        public bool? IndSex { get; set; }
        public bool? IndSab { get; set; }
        public bool? IndDom { get; set; }
        public bool? IndFeriado { get; set; }
        public int IndAtivo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [Required]
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column("NomeDispBBCriticidade")]
        [StringLength(20)]
        public string NomeDispBbcriticidade { get; set; }
        [Column("QTDDias")]
        public int? Qtddias { get; set; }
        [Column("SLAVirtual")]
        public double? Slavirtual { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
    }
}
