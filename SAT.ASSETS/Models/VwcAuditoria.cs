using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcAuditoria
    {
        public long? RowNumber { get; set; }
        public int CodAuditoria { get; set; }
        [StringLength(50)]
        public string CodUsuario { get; set; }
        public int? CodAuditoriaVeiculo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraRetiradaVeiculo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraCad { get; set; }
        public byte? CodAuditoriaStatus { get; set; }
        public int? TotalMesesEmUso { get; set; }
        public double? ValorCombustivel { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataRetiradaVeiculo { get; set; }
        public double? CreditosCartao { get; set; }
        [Column("DespesasSAT")]
        public double? DespesasSat { get; set; }
        public int? TotalDiasEmUso { get; set; }
        public double? DespesasCompensadasValor { get; set; }
        public double? OdometroInicialRetirada { get; set; }
        public double? OdometroPeriodoAuditado { get; set; }
        public double? SaldoCartao { get; set; }
        public int? KmPercorrido { get; set; }
        public int? KmCompensado { get; set; }
        public double? ValorTanque { get; set; }
        public int? KmFerias { get; set; }
        public double? UsoParticular { get; set; }
        public double? KmParticular { get; set; }
        public string Observacoes { get; set; }
        public double? KmParticularMes { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataHoraManut { get; set; }
        [StringLength(50)]
        public string CodUsuarioManut { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeUsuario { get; set; }
        [StringLength(50)]
        public string AuditoriaStatus { get; set; }
        public int CodFilial { get; set; }
        [Required]
        [StringLength(50)]
        public string NomeFilial { get; set; }
        [Column("QTDDespesasNaoAprovadas")]
        public int QtddespesasNaoAprovadas { get; set; }
    }
}
