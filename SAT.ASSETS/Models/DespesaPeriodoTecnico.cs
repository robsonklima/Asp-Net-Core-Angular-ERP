using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("DespesaPeriodoTecnico")]
    public partial class DespesaPeriodoTecnico
    {
        [Key]
        public int CodDespesaPeriodoTecnico { get; set; }
        public int CodDespesaPeriodo { get; set; }
        public int CodTecnico { get; set; }
        public int CodDespesaPeriodoTecnicoStatus { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(50)]
        public string CodUsuarioCredito { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCredito { get; set; }
        [StringLength(50)]
        public string CodUsuarioCreditoCancelado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCreditoCancelado { get; set; }
        public byte? IndCredito { get; set; }
        [StringLength(50)]
        public string CodUsuarioVerificacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraVerificacao { get; set; }
        public byte? IndVerificacao { get; set; }
        [StringLength(50)]
        public string CodUsuarioVerificacaoCancelado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraVerificacaoCancelado { get; set; }
        public byte? IndCompensacao { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCompensacao { get; set; }
        [StringLength(40)]
        public string CodUsuarioCompensacao { get; set; }

        [ForeignKey(nameof(CodDespesaPeriodo))]
        [InverseProperty(nameof(DespesaPeriodo.DespesaPeriodoTecnicos))]
        public virtual DespesaPeriodo CodDespesaPeriodoNavigation { get; set; }
        [ForeignKey(nameof(CodDespesaPeriodoTecnicoStatus))]
        [InverseProperty(nameof(DespesaPeriodoTecnicoStatus.DespesaPeriodoTecnicos))]
        public virtual DespesaPeriodoTecnicoStatus CodDespesaPeriodoTecnicoStatusNavigation { get; set; }
        [ForeignKey(nameof(CodTecnico))]
        [InverseProperty(nameof(Tecnico.DespesaPeriodoTecnicos))]
        public virtual Tecnico CodTecnicoNavigation { get; set; }
    }
}
