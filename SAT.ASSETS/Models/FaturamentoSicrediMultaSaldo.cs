using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("FaturamentoSicrediMultaSaldo")]
    public partial class FaturamentoSicrediMultaSaldo
    {
        public FaturamentoSicrediMultaSaldo()
        {
            FaturamentoSicrediMultaSaldoItemLancados = new HashSet<FaturamentoSicrediMultaSaldoItemLancado>();
            FaturamentoSicrediMultaSaldoItemMulta = new HashSet<FaturamentoSicrediMultaSaldoItemMultum>();
        }

        [Key]
        public int CodFaturamentoSicrediMultaSaldo { get; set; }
        public int CodFaturamentoSicredi { get; set; }
        public int CodCooperativaSicredi { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Multa { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Credito { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Saldo { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? Lancado { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }

        [ForeignKey(nameof(CodCooperativaSicredi))]
        [InverseProperty(nameof(CooperativaSicredi.FaturamentoSicrediMultaSaldos))]
        public virtual CooperativaSicredi CodCooperativaSicrediNavigation { get; set; }
        [ForeignKey(nameof(CodFaturamentoSicredi))]
        [InverseProperty(nameof(FaturamentoSicredi.FaturamentoSicrediMultaSaldos))]
        public virtual FaturamentoSicredi CodFaturamentoSicrediNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.FaturamentoSicrediMultaSaldos))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
        [InverseProperty(nameof(FaturamentoSicrediMultaSaldoItemLancado.CodFaturamentoSicrediMultaSaldoNavigation))]
        public virtual ICollection<FaturamentoSicrediMultaSaldoItemLancado> FaturamentoSicrediMultaSaldoItemLancados { get; set; }
        [InverseProperty(nameof(FaturamentoSicrediMultaSaldoItemMultum.CodFaturamentoSicrediMultaSaldoNavigation))]
        public virtual ICollection<FaturamentoSicrediMultaSaldoItemMultum> FaturamentoSicrediMultaSaldoItemMulta { get; set; }
    }
}
