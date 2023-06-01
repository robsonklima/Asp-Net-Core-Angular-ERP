using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("EquipContratoComparacao")]
    public partial class EquipContratoComparacao
    {
        [Key]
        public int CodEquipContratoComparacao { get; set; }
        public int? CodCliente { get; set; }
        [StringLength(50)]
        public string NumSerie { get; set; }
        public int? CodPosto { get; set; }
        public int? CodEquip { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public byte? IndEquipComparado { get; set; }
        [StringLength(20)]
        public string CodUsuarioComp { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraComp { get; set; }
        [Column("CodEqCComparacaoNome")]
        public int? CodEqCcomparacaoNome { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        [StringLength(5)]
        public string NumAgencia { get; set; }
        [Column("DCPosto")]
        [StringLength(2)]
        public string Dcposto { get; set; }
        [Column("SLA")]
        [StringLength(10)]
        public string Sla { get; set; }
        [Column("IndSEMAT")]
        public byte? IndSemat { get; set; }
        [StringLength(1)]
        public string PontoEstrategico { get; set; }
        [Column("IndRHorario")]
        public byte? IndRhorario { get; set; }
        [Column("IndRAcesso")]
        public byte? IndRacesso { get; set; }
        [Column("DistanciaKMPAT")]
        public int? DistanciaKmpat { get; set; }
    }
}
