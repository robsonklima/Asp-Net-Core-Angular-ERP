using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcReportingBanrisulChamadosOrcamento
    {
        [Column("OS")]
        public int Os { get; set; }
        [Column("NumOSCliente")]
        [StringLength(20)]
        public string NumOscliente { get; set; }
        [StringLength(50)]
        public string Intervenção { get; set; }
        [Required]
        [StringLength(50)]
        public string Filial { get; set; }
        [Required]
        [Column("PAT Regiao")]
        [StringLength(50)]
        public string PatRegiao { get; set; }
        [StringLength(10)]
        public string Agencia { get; set; }
        [Required]
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
        public string Observação { get; set; }
        [Column("PA")]
        public int? Pa { get; set; }
        [Column("SLA")]
        [StringLength(50)]
        public string Sla { get; set; }
        [Column("StatusOS")]
        [StringLength(50)]
        public string StatusOs { get; set; }
        [Column("DataHoraAberturaOS", TypeName = "datetime")]
        public DateTime? DataHoraAberturaOs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataAgendamento { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataFechamento { get; set; }
        [Column("DataFimSLA", TypeName = "datetime")]
        public DateTime DataFimSla { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime HoraInicial { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime HoraFinal { get; set; }
        public short? HorasTecnicas { get; set; }
        [Column("KM", TypeName = "decimal(10, 2)")]
        public decimal? Km { get; set; }
        [Required]
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [StringLength(1000)]
        public string RelatoSolucao { get; set; }
        [StringLength(30)]
        public string DataFiltro { get; set; }
        [Required]
        [Column("Maquina_Extra_Maquina")]
        [StringLength(13)]
        public string MaquinaExtraMaquina { get; set; }
        public int CodContrato { get; set; }
        public int CodEquip { get; set; }
    }
}
