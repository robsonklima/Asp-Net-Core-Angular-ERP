using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Parque_Resumo2")]
    public partial class ParqueResumo2
    {
        [Column("CodParque_Resumo")]
        public int CodParqueResumo { get; set; }
        public int? AnoMes { get; set; }
        public int? CodCliente { get; set; }
        public int? CodPosto { get; set; }
        public int? CodTipoEquip { get; set; }
        public int? CodGrupoEquip { get; set; }
        public int? CodEquip { get; set; }
        public int? CodRegiao { get; set; }
        public int? CodAutorizada { get; set; }
        public int? CodFilial { get; set; }
        [Column("CodSLA")]
        public int? CodSla { get; set; }
        public int? CodContrato { get; set; }
        public int? CodEquipContrato { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorReceita { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorDespesa { get; set; }
        [Column(TypeName = "money")]
        public decimal? ValorDespesaInstalacao { get; set; }
        public byte? IndAtivo { get; set; }
        public byte? IndReceita { get; set; }
        public byte? IndRepasse { get; set; }
        public byte? IndGarantia { get; set; }
        public byte? IndInstalacao { get; set; }
    }
}
