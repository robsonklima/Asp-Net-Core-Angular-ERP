using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Keyless]
    public partial class VwcRelSlaCefMesAnterior
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
        [Column("KM")]
        public int? Km { get; set; }
        [Column("StatusSLA")]
        [StringLength(15)]
        public string StatusSla { get; set; }
        [Column("MINUTOS")]
        public double? Minutos { get; set; }
        [Column("Horas Atraso")]
        [StringLength(7)]
        public string HorasAtraso { get; set; }
        [Column("Multa Perto", TypeName = "numeric(10, 2)")]
        public decimal MultaPerto { get; set; }
    }
}
