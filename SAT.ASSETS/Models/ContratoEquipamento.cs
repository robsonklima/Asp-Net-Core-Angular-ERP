using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ContratoEquipamento")]
    public partial class ContratoEquipamento
    {
        public ContratoEquipamento()
        {
            ContratoEquipMulta = new HashSet<ContratoEquipMultum>();
            ContratoEquipRetencaos = new HashSet<ContratoEquipRetencao>();
        }

        [Key]
        public int CodContrato { get; set; }
        [Key]
        public int CodTipoEquip { get; set; }
        [Key]
        public int CodGrupoEquip { get; set; }
        [Key]
        public int CodEquip { get; set; }
        public int QtdEquip { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal VlrUnitario { get; set; }
        public int CodTipoGarantia { get; set; }
        [Column("DataRecDM", TypeName = "datetime")]
        public DateTime? DataRecDm { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? VlrInstalacao { get; set; }
        public byte? IndGarPriSem { get; set; }
        public byte? IndGarSegSem { get; set; }
        public byte? IndGarTerSem { get; set; }
        public byte? IndGarQuaSem { get; set; }
        public byte? IndGarPriQui { get; set; }
        public byte? IndGarSegQui { get; set; }
        public int QtdDiaGarantia { get; set; }
        public int QtdLimDiaEnt { get; set; }
        public int? QtdLimDiaIns { get; set; }
        public int? CodContratoEquipDataGar { get; set; }
        public int CodContratoEquipDataEnt { get; set; }
        public int? CodContratoEquipDataIns { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataGar { get; set; }
        [Column("PercIPI", TypeName = "decimal(10, 2)")]
        public decimal? PercIpi { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PercValorEnt { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal PercValorIns { get; set; }
        [Column("DataInicioMTBF", TypeName = "datetime")]
        public DateTime? DataInicioMtbf { get; set; }
        [Column("DataFimMTBF", TypeName = "datetime")]
        public DateTime? DataFimMtbf { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(20)]
        public string CodMagnus { get; set; }

        [ForeignKey(nameof(CodContratoEquipDataEnt))]
        [InverseProperty(nameof(ContratoEquipDatum.ContratoEquipamentoCodContratoEquipDataEntNavigations))]
        public virtual ContratoEquipDatum CodContratoEquipDataEntNavigation { get; set; }
        [ForeignKey(nameof(CodContratoEquipDataGar))]
        [InverseProperty(nameof(ContratoEquipDatum.ContratoEquipamentoCodContratoEquipDataGarNavigations))]
        public virtual ContratoEquipDatum CodContratoEquipDataGarNavigation { get; set; }
        [ForeignKey(nameof(CodContratoEquipDataIns))]
        [InverseProperty(nameof(ContratoEquipDatum.ContratoEquipamentoCodContratoEquipDataInsNavigations))]
        public virtual ContratoEquipDatum CodContratoEquipDataInsNavigation { get; set; }
        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.ContratoEquipamentos))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodTipoGarantia))]
        [InverseProperty(nameof(TipoGarantium.ContratoEquipamentos))]
        public virtual TipoGarantium CodTipoGarantiaNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCad))]
        [InverseProperty(nameof(Usuario.ContratoEquipamentoCodUsuarioCadNavigations))]
        public virtual Usuario CodUsuarioCadNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioManut))]
        [InverseProperty(nameof(Usuario.ContratoEquipamentoCodUsuarioManutNavigations))]
        public virtual Usuario CodUsuarioManutNavigation { get; set; }
        [InverseProperty(nameof(ContratoEquipMultum.Cod))]
        public virtual ICollection<ContratoEquipMultum> ContratoEquipMulta { get; set; }
        [InverseProperty(nameof(ContratoEquipRetencao.Cod))]
        public virtual ICollection<ContratoEquipRetencao> ContratoEquipRetencaos { get; set; }
    }
}
