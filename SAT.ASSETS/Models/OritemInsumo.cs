using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("ORItemInsumo")]
    public partial class OritemInsumo
    {
        [Column("CodORItemInsumo")]
        public int CodOritemInsumo { get; set; }
        [Column("CodORItem")]
        public int? CodOritem { get; set; }
        [Column("DataHoraORItem", TypeName = "datetime")]
        public DateTime? DataHoraOritem { get; set; }
        [Column("CodOR")]
        public int? CodOr { get; set; }
        public int? CodPeca { get; set; }
        public int? CodStatus { get; set; }
        public int? Quantidade { get; set; }
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
        [StringLength(50)]
        public string CodDefeito { get; set; }
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
        public int? CodStatusPendente { get; set; }
        public int? IndLiberacao { get; set; }
    }
}
