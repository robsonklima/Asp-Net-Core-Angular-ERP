using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelAnaliseBasa2
    {
        [Column("OS")]
        public int? Os { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [Column("Existe Perto")]
        [StringLength(6)]
        public string ExistePerto { get; set; }
        [Column("OS Cliente")]
        public string OsCliente { get; set; }
        [Column("OS 4a")]
        public string Os4a { get; set; }
        public string Cliente { get; set; }
        public string Filial { get; set; }
        public string Contrato { get; set; }
        [Column("Contrato Perto")]
        [StringLength(30)]
        public string ContratoPerto { get; set; }
        [Column("Tipo Contrato")]
        public string TipoContrato { get; set; }
        public int CodContrato { get; set; }
        public string Agencia { get; set; }
        [Column("DC")]
        public string Dc { get; set; }
        public string Local { get; set; }
        public string Cidade { get; set; }
        [Column("UF")]
        public string Uf { get; set; }
        [Column("Status OS")]
        public string StatusOs { get; set; }
        public string Modelo { get; set; }
        [Required]
        [Column("Modelo Perto")]
        [StringLength(50)]
        public string ModeloPerto { get; set; }
        [Column("N Serie")]
        public string NSerie { get; set; }
        [Column("Serie Perto")]
        [StringLength(20)]
        public string SeriePerto { get; set; }
        [Column("Data Abert OS", TypeName = "datetime")]
        public DateTime? DataAbertOs { get; set; }
        [Column("Data Abertura Perto", TypeName = "datetime")]
        public DateTime? DataAberturaPerto { get; set; }
        [Required]
        [Column("Analise ABERTURA")]
        [StringLength(16)]
        public string AnaliseAbertura { get; set; }
        [Column("Data Solic", TypeName = "datetime")]
        public DateTime? DataSolic { get; set; }
        [Column("Data Cancelamento", TypeName = "datetime")]
        public DateTime? DataCancelamento { get; set; }
        [Column("Motivo Cancelamento")]
        public string MotivoCancelamento { get; set; }
        [Column("Data Fechamento", TypeName = "datetime")]
        public DateTime? DataFechamento { get; set; }
        [Column("Data Fechamento Perto", TypeName = "datetime")]
        public DateTime? DataFechamentoPerto { get; set; }
        [Required]
        [Column("Analise Fechamento")]
        [StringLength(9)]
        public string AnaliseFechamento { get; set; }
        public string Intervenção { get; set; }
        [Column("Intervenção Perto")]
        [StringLength(50)]
        public string IntervençãoPerto { get; set; }
        [Column("Dist PAT até Filial em Km")]
        public double? DistPatAtéFilialEmKm { get; set; }
        [Column("Data Agendamento Perto", TypeName = "datetime")]
        public DateTime? DataAgendamentoPerto { get; set; }
        [Column("Fim SLA", TypeName = "datetime")]
        public DateTime? FimSla { get; set; }
        [Column("SLA")]
        public double? Sla { get; set; }
        [Column("SLA Perto")]
        [StringLength(50)]
        public string SlaPerto { get; set; }
        [Column("DIAS")]
        public double? Dias { get; set; }
        public double? DiasUteis { get; set; }
        [Required]
        [Column("OBS")]
        [StringLength(1)]
        public string Obs { get; set; }
        [Required]
        [Column("Filial Perto")]
        [StringLength(50)]
        public string FilialPerto { get; set; }
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [Column("PAT")]
        public string Pat { get; set; }
        [Required]
        [Column("PAT Perto")]
        [StringLength(50)]
        public string PatPerto { get; set; }
        public string Região { get; set; }
        [Required]
        [Column("Região Perto")]
        [StringLength(50)]
        public string RegiãoPerto { get; set; }
        [Required]
        [Column("RAT Pendente")]
        [StringLength(3)]
        public string RatPendente { get; set; }
        [Column("Status SLA")]
        [StringLength(15)]
        public string StatusSla1 { get; set; }
        [Required]
        [Column("MAQUINA/EXTRA-MAQUINA")]
        [StringLength(13)]
        public string MaquinaExtraMaquina { get; set; }
        [Column("Tipo Causa")]
        public string TipoCausa { get; set; }
        [Column("Cód Causa")]
        public string CódCausa { get; set; }
        public string Causa { get; set; }
        [Column("Causa Perto")]
        [StringLength(70)]
        public string CausaPerto { get; set; }
        [Column("Defeito Relatado")]
        public string DefeitoRelatado { get; set; }
        public string Observação { get; set; }
        [Column("Num RAT")]
        public string NumRat { get; set; }
        [Column("Status RAT")]
        public string StatusRat { get; set; }
        public string Serviço { get; set; }
        [Column("Técnico Transf")]
        public string TécnicoTransf { get; set; }
        public string Técnico { get; set; }
        [Column("RG")]
        public string Rg { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Início { get; set; }
        [Column("Qtde Horas Técnicas", TypeName = "datetime")]
        public DateTime? QtdeHorasTécnicas { get; set; }
        [Column("Relato Solucao")]
        public string RelatoSolucao { get; set; }
        [Column("Obs RAT")]
        public string ObsRat { get; set; }
        public string Defeito { get; set; }
        public string Ação { get; set; }
        [Column("Cód Magnus")]
        public string CódMagnus { get; set; }
        public double? Descrição { get; set; }
        public string Qtd { get; set; }
        [Column("A_P")]
        public double? AP { get; set; }
        [Column("PA")]
        public string Pa { get; set; }
        [Column("SEMAT")]
        public string Semat { get; set; }
        [Column("Ponto Estratégico")]
        public string PontoEstratégico { get; set; }
        [Column("RHorario")]
        public string Rhorario { get; set; }
        [Column("RAcesso")]
        public string Racesso { get; set; }
        [Column("VALOR FATUR.", TypeName = "numeric(7, 2)")]
        public decimal ValorFatur { get; set; }
        [Column("ABERTURA")]
        [StringLength(30)]
        public string Abertura { get; set; }
        [Column("HORA AB.")]
        [StringLength(30)]
        public string HoraAb { get; set; }
        [Column("FECHAMENTO")]
        [StringLength(30)]
        public string Fechamento { get; set; }
        [Column("HORA FECH.")]
        [StringLength(30)]
        public string HoraFech { get; set; }
        [Column("DIAS UTEIS")]
        public double? DiasUteis1 { get; set; }
        [Column("TEMPO DE ATEND. (DIAS)")]
        public int? TempoDeAtendDias { get; set; }
        [Column("EXCEDENTE")]
        public double? Excedente { get; set; }
        [Column("MULTA", TypeName = "numeric(10, 2)")]
        public decimal Multa { get; set; }
    }
}
