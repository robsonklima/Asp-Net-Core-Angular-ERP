using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcConsAdiantMediaDespesa
    {
        public int CodTecnico { get; set; }
        [StringLength(50)]
        public string Tecnico { get; set; }
        [Column("CPF")]
        [StringLength(20)]
        public string Cpf { get; set; }
        [StringLength(3)]
        public string Banco { get; set; }
        [StringLength(10)]
        public string Agencia { get; set; }
        [StringLength(20)]
        public string ContaCorrente { get; set; }
        [StringLength(22)]
        public string EmailDefault { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MediaMensal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MediaQuinzenal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MediaSemanal { get; set; }
        public int SaldoAbertoLogixMensal { get; set; }
        public int SaldoAbertoLogixQuinzenal { get; set; }
        public int SaldoAbertoLogixSemanal { get; set; }
        [Column("RDsEmAbertoMensal", TypeName = "decimal(10, 2)")]
        public decimal? RdsEmAbertoMensal { get; set; }
        [Column("RDsEmAbertoQuinzenal", TypeName = "decimal(10, 2)")]
        public decimal? RdsEmAbertoQuinzenal { get; set; }
        [Column("RDsEmAbertoSemanal", TypeName = "decimal(10, 2)")]
        public decimal? RdsEmAbertoSemanal { get; set; }
        [Column("SaldoAdiantamentoSATMensal")]
        public int SaldoAdiantamentoSatmensal { get; set; }
        [Column("SaldoAdiantamentoSATQuinzenal")]
        public int SaldoAdiantamentoSatquinzenal { get; set; }
        [Column("SaldoAdiantamentoSATSemanal")]
        public int SaldoAdiantamentoSatsemanal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MaximoParaSolicitarMensal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MaximoParaSolicitarQuinzenal { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? MaximoParaSolicitarSemanal { get; set; }
    }
}
