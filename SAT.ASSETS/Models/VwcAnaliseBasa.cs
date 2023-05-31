using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcAnaliseBasa
    {
        [Column("OS")]
        public int? Os { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [Required]
        [Column("Existe Perto")]
        [StringLength(10)]
        public string ExistePerto { get; set; }
        [Column("CONTRATO")]
        [StringLength(100)]
        public string Contrato { get; set; }
        [Column("Contrato Perto")]
        [StringLength(30)]
        public string ContratoPerto { get; set; }
        public int CodContrato { get; set; }
        [Column("AGÊNCIA")]
        [StringLength(200)]
        public string Agência { get; set; }
        [Column("MUNICÍPIO")]
        [StringLength(100)]
        public string Município { get; set; }
        [Column("UF")]
        [StringLength(2)]
        public string Uf { get; set; }
        [Column("LOCALIZAÇÃO")]
        [StringLength(50)]
        public string Localização { get; set; }
        [Column("FATOR")]
        [StringLength(50)]
        public string Fator { get; set; }
        [Column("REDUTOR")]
        public int? Redutor { get; set; }
        [Column("MODELO")]
        [StringLength(50)]
        public string Modelo { get; set; }
        [Required]
        [Column("Modelo Perto")]
        [StringLength(50)]
        public string ModeloPerto { get; set; }
        [Column("Nº SÉRIE")]
        [StringLength(50)]
        public string NºSérie { get; set; }
        [Column("Série Perto")]
        [StringLength(20)]
        public string SériePerto { get; set; }
        [Column("VALOR FATUR", TypeName = "money")]
        public decimal? ValorFatur { get; set; }
        [Column("ABERTURA", TypeName = "datetime")]
        public DateTime? Abertura { get; set; }
        [Column("HORA AB")]
        [StringLength(50)]
        public string HoraAb { get; set; }
        [Column("DATAHORAABERTURA", TypeName = "datetime")]
        public DateTime? Datahoraabertura { get; set; }
        [Column("Data Abertura Perto", TypeName = "datetime")]
        public DateTime? DataAberturaPerto { get; set; }
        [Required]
        [Column("Analise ABERTURA")]
        [StringLength(16)]
        public string AnaliseAbertura { get; set; }
        [Column("FECHAMENTO", TypeName = "datetime")]
        public DateTime? Fechamento { get; set; }
        [Column("HORA FECH")]
        [StringLength(50)]
        public string HoraFech { get; set; }
        [Column("DATAHORAFECHAMENTO", TypeName = "datetime")]
        public DateTime? Datahorafechamento { get; set; }
        [Column("Data Fechamento Perto", TypeName = "datetime")]
        public DateTime? DataFechamentoPerto { get; set; }
        [Required]
        [Column("Analise Fechamento")]
        [StringLength(9)]
        public string AnaliseFechamento { get; set; }
        [StringLength(50)]
        public string TipoIntervencao { get; set; }
        [Column("Data Agendamento Perto", TypeName = "datetime")]
        public DateTime? DataAgendamentoPerto { get; set; }
        [Column("Fim SLA", TypeName = "datetime")]
        public DateTime? FimSla { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Column("DIAS")]
        public double? Dias { get; set; }
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
        [Required]
        [Column("PAT Perto")]
        [StringLength(50)]
        public string PatPerto { get; set; }
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
        [StringLength(70)]
        public string Causa { get; set; }
        [Column("DIAS UTEIS")]
        public int? DiasUteis { get; set; }
        [Column("TEMPO DE ATEND DIAS")]
        public double? TempoDeAtendDias { get; set; }
        [Column("EXCEDENTE")]
        [StringLength(100)]
        public string Excedente { get; set; }
        [Column("TEMPO UD")]
        public double? TempoUd { get; set; }
        [Column("MULTA", TypeName = "money")]
        public decimal? Multa { get; set; }
    }
}
