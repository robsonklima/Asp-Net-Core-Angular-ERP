using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelAnaliseCaixaCofre
    {
        [StringLength(255)]
        public string Chamado { get; set; }
        [Column("OS Cliente Perto")]
        [StringLength(20)]
        public string OsClientePerto { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Periodo { get; set; }
        [Column("RAT")]
        public double? Rat { get; set; }
        [Required]
        [Column("Existe Perto")]
        [StringLength(10)]
        public string ExistePerto { get; set; }
        [Column("CodOS")]
        public int CodOs { get; set; }
        [StringLength(50)]
        public string TipoIntervencao { get; set; }
        [Column("Data Abertura Perto", TypeName = "datetime")]
        public DateTime? DataAberturaPerto { get; set; }
        [Column("Data Agendamento Perto", TypeName = "datetime")]
        public DateTime? DataAgendamentoPerto { get; set; }
        [Column("Data Fechamento Perto", TypeName = "datetime")]
        public DateTime? DataFechamentoPerto { get; set; }
        [Column("Data Fechamento PA", TypeName = "datetime")]
        public DateTime? DataFechamentoPa { get; set; }
        public int? Distância { get; set; }
        [Column("Fim SLA", TypeName = "datetime")]
        public DateTime? FimSla { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Column("MODALIDADE")]
        [StringLength(255)]
        public string Modalidade { get; set; }
        [Column("CO_SERVICO")]
        public double? CoServico { get; set; }
        [Column("DE_SERVICO")]
        [StringLength(255)]
        public string DeServico { get; set; }
        [Column("VR_SERVICO")]
        [StringLength(255)]
        public string VrServico { get; set; }
        [Column("VL_MULTA_UNIT")]
        [StringLength(255)]
        public string VlMultaUnit { get; set; }
        [Column("ATRASO_MINUTOS")]
        [StringLength(255)]
        public string AtrasoMinutos { get; set; }
        [Column("ATRASO")]
        [StringLength(255)]
        public string Atraso { get; set; }
        [Column("VR_MULTA")]
        [StringLength(255)]
        public string VrMulta { get; set; }
        [Column("MINUTOS")]
        public double? Minutos { get; set; }
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
        [Column("Perto - Atraso_Minutos")]
        public double? PertoAtrasoMinutos { get; set; }
        [Column("Perto - Atraso_Horas")]
        [StringLength(7)]
        public string PertoAtrasoHoras { get; set; }
        [Column("Perto - Multa", TypeName = "numeric(10, 2)")]
        public decimal PertoMulta { get; set; }
        [Required]
        [StringLength(9)]
        public string Comparativo { get; set; }
    }
}
