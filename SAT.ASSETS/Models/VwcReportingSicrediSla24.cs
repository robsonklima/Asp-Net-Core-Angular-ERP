using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingSicrediSla24
    {
        [Column("OS")]
        public int Os { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [StringLength(50)]
        public string Intervencao { get; set; }
        [Required]
        [StringLength(7)]
        public string Cliente { get; set; }
        [Required]
        [StringLength(50)]
        public string Filial { get; set; }
        [Required]
        [Column("PAT")]
        [StringLength(50)]
        public string Pat { get; set; }
        [Required]
        [StringLength(50)]
        public string Regiao { get; set; }
        [StringLength(10)]
        public string Agencia { get; set; }
        [Required]
        [Column("_Local")]
        [StringLength(50)]
        public string Local { get; set; }
        [Required]
        [StringLength(50)]
        public string Cidade { get; set; }
        [Required]
        [Column("UF")]
        [StringLength(50)]
        public string Uf { get; set; }
        [Required]
        [StringLength(50)]
        public string Equipamento { get; set; }
        [Column("NSerie")]
        [StringLength(20)]
        public string Nserie { get; set; }
        [StringLength(3500)]
        public string DefeitoRelatado { get; set; }
        public string Observacao { get; set; }
        [Column("PA")]
        public int? Pa { get; set; }
        [Required]
        [Column("SEMAT")]
        [StringLength(3)]
        public string Semat { get; set; }
        [Required]
        [StringLength(3)]
        public string PontoEstrategico { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Column("StatusOS")]
        [StringLength(50)]
        public string StatusOs { get; set; }
        [Column("DataAberturaOS")]
        [StringLength(30)]
        public string DataAberturaOs { get; set; }
        [StringLength(30)]
        public string DataAgendamento { get; set; }
        [StringLength(30)]
        public string DataFechamento { get; set; }
        [Column("DataFimSLA", TypeName = "datetime")]
        public DateTime? DataFimSla { get; set; }
        [Column("KM")]
        public int? Km { get; set; }
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [Required]
        [Column("Maquina_Extra_Maquina")]
        [StringLength(13)]
        public string MaquinaExtraMaquina { get; set; }
        [StringLength(30)]
        public string TempoAtendimento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataFiltro { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
    }
}
