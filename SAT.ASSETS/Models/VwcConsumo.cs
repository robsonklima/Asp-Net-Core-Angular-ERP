using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcConsumo
    {
        [Required]
        [StringLength(50)]
        public string Cliente { get; set; }
        [StringLength(100)]
        public string Contrato { get; set; }
        [Required]
        [StringLength(50)]
        public string Filial { get; set; }
        [StringLength(50)]
        public string Técnico { get; set; }
        [StringLength(24)]
        public string Magnus { get; set; }
        [Column("Descrição Peça")]
        [StringLength(80)]
        public string DescriçãoPeça { get; set; }
        [Column("Valor Custo", TypeName = "decimal(10, 2)")]
        public decimal? ValorCusto { get; set; }
        [Column("Quantidade Peças")]
        public int? QuantidadePeças { get; set; }
        public int? TotalEquipFilial { get; set; }
        public int? TotalEquip { get; set; }
        [StringLength(50)]
        public string Ação { get; set; }
        public int CodFilial { get; set; }
        public int CodCliente { get; set; }
        public int CodAcao { get; set; }
        public int CodTecnico { get; set; }
        [Column("Dt Abert OS", TypeName = "datetime")]
        public DateTime? DtAbertOs { get; set; }
        [Column("Num OS")]
        public int NumOs { get; set; }
        public int? CodContrato { get; set; }
        [StringLength(20)]
        public string Pai { get; set; }
        [Column("CodRAT")]
        public int CodRat { get; set; }
        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }
        [Required]
        [Column("_UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [StringLength(7)]
        public string Mes { get; set; }
        public int CodTipoIntervencao { get; set; }
        [StringLength(20)]
        public string CpfLogix { get; set; }
        [StringLength(50)]
        public string NomTipoIntervencao { get; set; }
        public int? CodEquip { get; set; }
    }
}
