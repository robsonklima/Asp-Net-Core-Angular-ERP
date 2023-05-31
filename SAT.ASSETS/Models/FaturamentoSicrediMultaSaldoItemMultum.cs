using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    public partial class FaturamentoSicrediMultaSaldoItemMultum
    {
        [Key]
        public int CodFaturamentoSicrediMultaSaldoItemMulta { get; set; }
        public int CodFaturamentoSicrediMultaSaldo { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuario { get; set; }

        [ForeignKey(nameof(CodFaturamentoSicrediMultaSaldo))]
        [InverseProperty(nameof(FaturamentoSicrediMultaSaldo.FaturamentoSicrediMultaSaldoItemMulta))]
        public virtual FaturamentoSicrediMultaSaldo CodFaturamentoSicrediMultaSaldoNavigation { get; set; }
        [ForeignKey(nameof(CodOs))]
        [InverseProperty(nameof(O.FaturamentoSicrediMultaSaldoItemMulta))]
        public virtual O CodOsNavigation { get; set; }
        [ForeignKey(nameof(CodUsuario))]
        [InverseProperty(nameof(Usuario.FaturamentoSicrediMultaSaldoItemMulta))]
        public virtual Usuario CodUsuarioNavigation { get; set; }
    }
}
