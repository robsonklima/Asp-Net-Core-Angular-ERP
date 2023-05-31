using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelMultaCef2
    {
        [Column("RAT")]
        [StringLength(50)]
        public string Rat { get; set; }
        [Column("Data de Homologação", TypeName = "datetime")]
        public DateTime? DataDeHomologação { get; set; }
        [Column("Data de Agendamento", TypeName = "datetime")]
        public DateTime? DataDeAgendamento { get; set; }
        [Column("Início do Atendimento", TypeName = "datetime")]
        public DateTime? InícioDoAtendimento { get; set; }
        [Column("Término do Atendimento", TypeName = "datetime")]
        public DateTime? TérminoDoAtendimento { get; set; }
        [StringLength(50)]
        public string Chamado { get; set; }
        [StringLength(200)]
        public string Ocorrência { get; set; }
        [Column("Código do Orçamento")]
        [StringLength(50)]
        public string CódigoDoOrçamento { get; set; }
        [StringLength(50)]
        public string Lote { get; set; }
        [Column("CGC Unidade/Convênio")]
        [StringLength(50)]
        public string CgcUnidadeConvênio { get; set; }
        [Column("Nome da Unidade")]
        [StringLength(50)]
        public string NomeDaUnidade { get; set; }
        [StringLength(50)]
        public string Filial { get; set; }
        [StringLength(50)]
        public string Modalidade { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Cobrado { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Glosas { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Multas { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Redutores { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Total { get; set; }
        [Column("Perto Multa", TypeName = "numeric(11, 3)")]
        public decimal PertoMulta { get; set; }
        [Column("Horas Atraso")]
        [StringLength(7)]
        public string HorasAtraso { get; set; }
        [Required]
        [StringLength(9)]
        public string Comparativo { get; set; }
        [Required]
        [Column("Perto Motivo")]
        [StringLength(54)]
        public string PertoMotivo { get; set; }
        [Column("Perto - Detalhe")]
        public string PertoDetalhe { get; set; }
        [Column("Perto - Data Abertura", TypeName = "datetime")]
        public DateTime? PertoDataAbertura { get; set; }
        [Column("Perto - Data Agendamento", TypeName = "datetime")]
        public DateTime? PertoDataAgendamento { get; set; }
        [Column("Perto - Data Fechamento", TypeName = "datetime")]
        public DateTime? PertoDataFechamento { get; set; }
        [Required]
        [Column("Filial Perto")]
        [StringLength(50)]
        public string FilialPerto { get; set; }
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
        [Required]
        [Column("Maquina/Extra-Maquina")]
        [StringLength(13)]
        public string MaquinaExtraMaquina { get; set; }
        [Column("Tipo Intervenção")]
        [StringLength(50)]
        public string TipoIntervenção { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
    }
}
