using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("NotaFaturamentoSicredi")]
    public partial class NotaFaturamentoSicredi
    {
        [Key]
        public int CodNotaFaturamentoSicredi { get; set; }
        public int CodCooperativaSicredi { get; set; }
        public int CodRepresentante { get; set; }
        [Column(TypeName = "date")]
        public DateTime MesAnoReferencia { get; set; }
        [Required]
        [Column("NumeroNF")]
        [StringLength(50)]
        public string NumeroNf { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCadastro { get; set; }

        [ForeignKey(nameof(CodCooperativaSicredi))]
        [InverseProperty(nameof(CooperativaSicredi.NotaFaturamentoSicredis))]
        public virtual CooperativaSicredi CodCooperativaSicrediNavigation { get; set; }
        [ForeignKey(nameof(CodRepresentante))]
        [InverseProperty(nameof(Representante.NotaFaturamentoSicredis))]
        public virtual Representante CodRepresentanteNavigation { get; set; }
        [ForeignKey(nameof(CodUsuarioCadastro))]
        [InverseProperty(nameof(Usuario.NotaFaturamentoSicredis))]
        public virtual Usuario CodUsuarioCadastroNavigation { get; set; }
    }
}
