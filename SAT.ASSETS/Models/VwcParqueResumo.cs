using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcParqueResumo
    {
        public int? AnoMes { get; set; }
        public int? CodEquipContrato { get; set; }
        public int? CodTipoContrato { get; set; }
        [StringLength(87)]
        public string AgenciaNumSerie { get; set; }
        [StringLength(20)]
        public string NumSerie { get; set; }
        public int? CodCliente { get; set; }
        public int? CodContrato { get; set; }
        [Column("AgenciaSB")]
        [StringLength(11)]
        public string AgenciaSb { get; set; }
        [StringLength(50)]
        public string NomeLocal { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        public byte? IndGarantia { get; set; }
        public byte? IndReceita { get; set; }
        public byte? IndRepasse { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorReceita { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorReceitaProRata { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorDespesa { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorDespesaProRata { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorDespesaInstalacao { get; set; }
        public byte? IndInstalacao { get; set; }
        public byte? IndAtivo { get; set; }
        [Column("SiglaUF")]
        [StringLength(50)]
        public string SiglaUf { get; set; }
        [StringLength(50)]
        public string NomePais { get; set; }
        [StringLength(50)]
        public string Lote { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Required]
        [StringLength(3)]
        public string Semat { get; set; }
        [StringLength(50)]
        public string NomeTipoContrato { get; set; }
    }
}
