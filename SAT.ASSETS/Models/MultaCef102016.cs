using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    [Table("Multa_CEF_10_2016")]
    public partial class MultaCef102016
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
        [Column("CGC Unidade_Convênio")]
        [StringLength(50)]
        public string CgcUnidadeConvênio { get; set; }
        [Column("Nome da Unidade")]
        [StringLength(50)]
        public string NomeDaUnidade { get; set; }
        [StringLength(50)]
        public string Filial { get; set; }
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
    }
}
