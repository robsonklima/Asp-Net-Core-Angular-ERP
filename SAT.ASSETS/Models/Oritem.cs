using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ORItem")]
    public partial class Oritem
    {
        [Column("CodORItem")]
        public int CodOritem { get; set; }
        [Column("DataHoraORItem", TypeName = "datetime")]
        public DateTime DataHoraOritem { get; set; }
        [Column("CodOR")]
        public int CodOr { get; set; }
        public int CodPeca { get; set; }
        public int CodStatus { get; set; }
        public int Quantidade { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        [Column("CodTipoOR")]
        public int? CodTipoOr { get; set; }
        [Column("CodOS")]
        public int? CodOs { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(150)]
        public string CodTecnico { get; set; }
        [StringLength(200)]
        public string DefeitoRelatado { get; set; }
        [StringLength(200)]
        public string RelatoSolucao { get; set; }
        public int? CodDefeito { get; set; }
        public int? CodAcao { get; set; }
        public int? CodSolucao { get; set; }
        public byte? IndConfLog { get; set; }
        public byte? IndConfLab { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(300)]
        public string DivergenciaDescricao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataConfLab { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataConfLog { get; set; }
        [Column("CodstatusOR")]
        public int? CodstatusOr { get; set; }
        public int? IndPrioridade { get; set; }
    }
}
