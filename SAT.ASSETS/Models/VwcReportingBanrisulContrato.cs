using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingBanrisulContrato
    {
        [Column("OS")]
        public int Os { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [StringLength(50)]
        public string Intervencao { get; set; }
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
        [StringLength(11)]
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
        public string DefeitoRelatado { get; set; }
        public string Observacao { get; set; }
        [Column("PA")]
        public int? Pa { get; set; }
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
        [Column("DataFimSLA")]
        [StringLength(30)]
        public string DataFimSla { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraInicial { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HoraSolucao { get; set; }
        [StringLength(7)]
        public string HorasTecnicas { get; set; }
        [Column("KM", TypeName = "decimal(10, 2)")]
        public decimal? Km { get; set; }
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [StringLength(7)]
        public string HorasAtraso { get; set; }
        public double? MinutosAtraso { get; set; }
        [StringLength(30)]
        public string DataFiltro { get; set; }
        [StringLength(2000)]
        public string RelatoSolucao { get; set; }
        [Required]
        [Column("Maquina_Extra_Maquina")]
        [StringLength(13)]
        public string MaquinaExtraMaquina { get; set; }
        public int CodContrato { get; set; }
        public int CodEquip { get; set; }
        [Column("ObsRAT")]
        [StringLength(1000)]
        public string ObsRat { get; set; }
    }
}
