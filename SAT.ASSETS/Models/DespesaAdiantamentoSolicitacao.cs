using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("DespesaAdiantamentoSolicitacao")]
    public partial class DespesaAdiantamentoSolicitacao
    {
        public int CodDespesaAdiantamentoSolicitacao { get; set; }
        public int CodTecnico { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        [Column("CPF")]
        [StringLength(50)]
        public string Cpf { get; set; }
        [Required]
        [StringLength(10)]
        public string Banco { get; set; }
        [Required]
        [StringLength(10)]
        public string Agencia { get; set; }
        [Required]
        [StringLength(20)]
        public string ContaCorrente { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal SaldoLogix { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MediaMensal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MediaQuinzenal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MediaSemanal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal SaldoAbertoLogixMensal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal SaldoAbertoLogixQuinzenal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal SaldoAbertoLogixSemanal { get; set; }
        [Column("RDsEmAbertoMensal", TypeName = "decimal(10, 2)")]
        public decimal RdsEmAbertoMensal { get; set; }
        [Column("RDsEmAbertoQuinzenal", TypeName = "decimal(10, 2)")]
        public decimal RdsEmAbertoQuinzenal { get; set; }
        [Column("RDsEmAbertoSemanal", TypeName = "decimal(10, 2)")]
        public decimal RdsEmAbertoSemanal { get; set; }
        [Column("SaldoAdiantamentoSATMensal", TypeName = "decimal(10, 2)")]
        public decimal SaldoAdiantamentoSatmensal { get; set; }
        [Column("SaldoAdiantamentoSATQuinzenal", TypeName = "decimal(10, 2)")]
        public decimal SaldoAdiantamentoSatquinzenal { get; set; }
        [Column("SaldoAdiantamentoSATSemanal", TypeName = "decimal(10, 2)")]
        public decimal SaldoAdiantamentoSatsemanal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MaximoParaSolicitarMensal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MaximoParaSolicitarQuinzenal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal MaximoParaSolicitarSemanal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal ValorAdiantamentoSolicitado { get; set; }
        public string Justificativa { get; set; }
        [Required]
        public string Emails { get; set; }
        [Required]
        [StringLength(20)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataHoraCad { get; set; }
    }
}
