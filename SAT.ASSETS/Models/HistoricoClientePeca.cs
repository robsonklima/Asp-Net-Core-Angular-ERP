using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("HistoricoClientePeca")]
    public partial class HistoricoClientePeca
    {
        [Key]
        public int CodClientePeca { get; set; }
        public int CodCliente { get; set; }
        public int CodContrato { get; set; }
        public int CodPeca { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? ValorUnitario { get; set; }
        [Column("ValorIPI", TypeName = "decimal(10, 2)")]
        public decimal? ValorIpi { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? VlrSubstituicaoNovo { get; set; }
        [Column("vlrBaseTroca", TypeName = "decimal(10, 2)")]
        public decimal? VlrBaseTroca { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioManut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraManut { get; set; }

        [ForeignKey(nameof(CodCliente))]
        [InverseProperty(nameof(Cliente.HistoricoClientePecas))]
        public virtual Cliente CodClienteNavigation { get; set; }
        [ForeignKey(nameof(CodContrato))]
        [InverseProperty(nameof(Contrato.HistoricoClientePecas))]
        public virtual Contrato CodContratoNavigation { get; set; }
    }
}
