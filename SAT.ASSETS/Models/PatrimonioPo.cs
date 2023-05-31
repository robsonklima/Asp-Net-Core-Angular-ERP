using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("PatrimonioPOS")]
    public partial class PatrimonioPo
    {
        [Key]
        [Column("CodPatrimonioPOS")]
        public int CodPatrimonioPos { get; set; }
        public int NumeroPatrimonio { get; set; }
        public int NotaFiscal { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataEmissao { get; set; }
        public int CodSolicitantePatrimonio { get; set; }
        public int CodCooperativaSicredi { get; set; }
        public int CodRepresentante { get; set; }
        public int CodEquip { get; set; }
        public int CodGrupoEquip { get; set; }
        public int CodTipoEquip { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataDevolucao { get; set; }
        [Column("NumOSSicredi")]
        [StringLength(50)]
        public string NumOssicredi { get; set; }
        public bool? PendenciaItem { get; set; }
        public bool? PendenciaDeclaracao { get; set; }
        [StringLength(8000)]
        public string Observacao { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataAlteracao { get; set; }

        [ForeignKey("CodEquip,CodGrupoEquip,CodTipoEquip")]
        [InverseProperty(nameof(Equipamento.PatrimonioPos))]
        public virtual Equipamento Cod { get; set; }
        [ForeignKey(nameof(CodCooperativaSicredi))]
        [InverseProperty(nameof(CooperativaSicredi.PatrimonioPos))]
        public virtual CooperativaSicredi CodCooperativaSicrediNavigation { get; set; }
        [ForeignKey(nameof(CodRepresentante))]
        [InverseProperty(nameof(Representante.PatrimonioPos))]
        public virtual Representante CodRepresentanteNavigation { get; set; }
        [ForeignKey(nameof(CodSolicitantePatrimonio))]
        [InverseProperty(nameof(SolicitantePatrimonio.PatrimonioPos))]
        public virtual SolicitantePatrimonio CodSolicitantePatrimonioNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.PatrimonioPos))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
