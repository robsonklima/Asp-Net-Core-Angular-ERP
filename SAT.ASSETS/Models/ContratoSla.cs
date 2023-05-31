using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("ContratoSLA")]
    public partial class ContratoSla
    {
        [Key]
        public int CodContrato { get; set; }
        [Key]
        [Column("CodSLA")]
        public int CodSla { get; set; }
        public byte? IndAgendamento { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }

        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.ContratoSlas))]
        public virtual Contrato CodContratoNavigation { get; set; }
        [ForeignKey(nameof(CodSla))]
        [InverseProperty(nameof(Sla.ContratoSlas))]
        public virtual Sla CodSlaNavigation { get; set; }
    }
}
