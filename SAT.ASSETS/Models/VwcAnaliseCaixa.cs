using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcAnaliseCaixa
    {
        [StringLength(50)]
        public string Chamado { get; set; }
        [Column("OS Cliente Perto")]
        [StringLength(20)]
        public string OsClientePerto { get; set; }
        [Column("RAT")]
        public double? Rat { get; set; }
        [Column("Data de Homologação", TypeName = "datetime")]
        public DateTime? DataDeHomologação { get; set; }
        [Required]
        [Column("Existe Perto")]
        [StringLength(31)]
        public string ExistePerto { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [StringLength(50)]
        public string TipoIntervencao { get; set; }
        [Column("Data de Agendamento", TypeName = "datetime")]
        public DateTime? DataDeAgendamento { get; set; }
        [Column("Data Agendamento Perto", TypeName = "datetime")]
        public DateTime? DataAgendamentoPerto { get; set; }
        [Column("Analise Agendamento")]
        [StringLength(9)]
        public string AnaliseAgendamento { get; set; }
        [Column("Início do Atendimento", TypeName = "datetime")]
        public DateTime? InícioDoAtendimento { get; set; }
        [Column("Data Abertura Perto", TypeName = "datetime")]
        public DateTime? DataAberturaPerto { get; set; }
        [Column("Analise ABERTURA")]
        [StringLength(16)]
        public string AnaliseAbertura { get; set; }
        [Column("Término do Atendimento", TypeName = "datetime")]
        public DateTime? TérminoDoAtendimento { get; set; }
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
        [StringLength(50)]
        public string Sla { get; set; }
        [StringLength(255)]
        public string Ocorrência { get; set; }
        [Column("Código do Orçamento")]
        [StringLength(255)]
        public string CódigoDoOrçamento { get; set; }
        [StringLength(255)]
        public string Lote { get; set; }
        [Column("CGC Unidade/Convênio")]
        [StringLength(255)]
        public string CgcUnidadeConvênio { get; set; }
        [Column("Nome da Unidade")]
        [StringLength(255)]
        public string NomeDaUnidade { get; set; }
        [StringLength(255)]
        public string Filial { get; set; }
        [StringLength(255)]
        public string Modalidade { get; set; }
        public double? Cobrado { get; set; }
        public double? Glosas { get; set; }
        [Column("MINUTOS")]
        public double? Minutos { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal? Multas { get; set; }
        public double? Redutores { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal? Total { get; set; }
        [Column("OBS")]
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
    }
}
