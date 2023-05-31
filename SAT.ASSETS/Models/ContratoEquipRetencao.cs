using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ContratoEquipRetencao")]
    public partial class ContratoEquipRetencao
    {
        [Key]
        public int CodContratoEquipRetencao { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodEquip { get; set; }
        public int? CodTipoRetencao { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PercRetencao { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }

        [ForeignKey("CodContrato,CodTipoEquip,CodGrupoEquip,CodEquip")]
        [InverseProperty(nameof(ContratoEquipamento.ContratoEquipRetencaos))]
        public virtual ContratoEquipamento Cod { get; set; }
        [ForeignKey(nameof(CodTipoRetencao))]
        [InverseProperty(nameof(TipoRetencao.ContratoEquipRetencaos))]
        public virtual TipoRetencao CodTipoRetencaoNavigation { get; set; }
    }
}
