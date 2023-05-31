using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcAnaliseCaixa20062009
    {
        [StringLength(50)]
        public string Siate { get; set; }
        [Column("OS Cliente Perto")]
        [StringLength(20)]
        public string OsClientePerto { get; set; }
        [Column("Mês_Ano", TypeName = "datetime")]
        public DateTime? MêsAno { get; set; }
        [Required]
        [Column("Existe Perto")]
        [StringLength(10)]
        public string ExistePerto { get; set; }
        [Column("OS")]
        public int Os { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [StringLength(50)]
        public string Modelo { get; set; }
        [Required]
        [Column("Modelo Perto")]
        [StringLength(50)]
        public string ModeloPerto { get; set; }
        [StringLength(50)]
        public string Série { get; set; }
        [Column("Série Perto")]
        [StringLength(20)]
        public string SériePerto { get; set; }
        [StringLength(50)]
        public string TipoIntervencao { get; set; }
        [Column("Data Agendamento Perto", TypeName = "datetime")]
        public DateTime? DataAgendamentoPerto { get; set; }
        [Column("Data Abertura", TypeName = "datetime")]
        public DateTime? DataAbertura { get; set; }
        [Column("Data Abertura Perto", TypeName = "datetime")]
        public DateTime? DataAberturaPerto { get; set; }
        [Column("Analise ABERTURA")]
        [StringLength(16)]
        public string AnaliseAbertura { get; set; }
        [Column("Data Fechamento", TypeName = "datetime")]
        public DateTime? DataFechamento { get; set; }
        [Column("Data Fechamento Perto", TypeName = "datetime")]
        public DateTime? DataFechamentoPerto { get; set; }
        [Column("Data Fechamento PA", TypeName = "datetime")]
        public DateTime? DataFechamentoPa { get; set; }
        [Column("Analise Fechamento PA")]
        [StringLength(9)]
        public string AnaliseFechamentoPa { get; set; }
        [Column("Analise Fechamento")]
        [StringLength(9)]
        public string AnaliseFechamento { get; set; }
        public int? Distância { get; set; }
        [Column("Fim SLA", TypeName = "datetime")]
        public DateTime? FimSla { get; set; }
        [Column("SLA")]
        [StringLength(2)]
        public string Sla { get; set; }
        [Column("MINUTOS")]
        public double? Minutos { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Multa { get; set; }
        [Column("OBS")]
        [StringLength(1000)]
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
        [Column("IndRAcesso")]
        public byte? IndRacesso { get; set; }
    }
}
