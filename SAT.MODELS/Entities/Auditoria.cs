using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{

    public class Auditoria
    {
        public int CodAuditoria { get; set; }
        public string CodUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int? CodAuditoriaVeiculo { get; set; }
        public AuditoriaVeiculo AuditoriaVeiculo { get; set; }
        public DateTime? DataHoraRetiradaVeiculo { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public byte CodAuditoriaStatus { get; set; }
		public AuditoriaStatus AuditoriaStatus { get; set; }
        public int? TotalMesesEmUso { get; set; }
        public double? ValorCombustivel { get; set; }
        public DateTime? DataRetiradaVeiculo { get; set; }
        public double? CreditosCartao { get; set; }
        public double? DespesasSAT { get; set; }
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
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioManut { get; set; }
        public int? QtdDespesasPendentes { get; set; }
        [ForeignKey("CodAuditoria")]
        public List<AuditoriaFoto> Fotos  { get; set; }
    }
}

