using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ContratoReajuste")]
    public partial class ContratoReajuste
    {
        [Key]
        public int CodContratoReajuste { get; set; }
        public int CodContrato { get; set; }
        public int CodTipoIndiceReajuste { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PercReajuste { get; set; }
        public byte IndAtivo { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.ContratoReajustes))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodTipoIndiceReajuste))]
        [InverseProperty(nameof(TipoIndiceReajuste.ContratoReajustes))]
        public virtual TipoIndiceReajuste CodTipoIndiceReajusteNavigation { get; set; }
    }
}
