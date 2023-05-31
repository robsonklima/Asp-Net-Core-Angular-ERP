using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SAT.MVC.Model
{
    [Table("SLA_NEW")]
    public partial class SlaNew
    {
        public SlaNew()
        {
            SladistanciaTempos = new HashSet<SladistanciaTempo>();
        }

        [Key]
        [Column("CodSLA")]
        public int CodSla { get; set; }
        [Required]
        [Column("NomeSLA")]
        [StringLength(50)]
        public string NomeSla { get; set; }
        [Column("DescSLA")]
        [StringLength(500)]
        public string DescSla { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HorarioInicio { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? HorarioFim { get; set; }
        public int? TempoInicio { get; set; }
        public int? TempoReparo { get; set; }
        public int? TempoSolucao { get; set; }
        public bool? IndAgendamento { get; set; }
        public bool? IndHorasUteis { get; set; }
        public bool? IndSegunda { get; set; }
        public bool? IndTerca { get; set; }
        public bool? IndQuarta { get; set; }
        public bool? IndQuinta { get; set; }
        public bool? IndSexta { get; set; }
        public bool? IndSabado { get; set; }
        public bool? IndDomingo { get; set; }
        public bool? IndFeriado { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataCadastro { get; set; }
        [StringLength(50)]
        public string CodUsuarioCad { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DataManutencao { get; set; }
        [StringLength(50)]
        public string CodUsuarioManutencao { get; set; }
        public bool? IndFeriadoEstadual { get; set; }
        public bool? IndFeriadoNacional { get; set; }
        public bool? IndFeriadoMunicipal { get; set; }
        public bool? IndRestricaoAcesso { get; set; }
        public bool? IndRestricaoHorario { get; set; }
        public bool? IndSemat { get; set; }
        public bool? IndPontoExterno { get; set; }
        public int? IndPontoEstrategico { get; set; }
        public int? TempoRestricaoAcesso { get; set; }
        public int? TempoRestricaoHorario { get; set; }
        public int? TempoSemat { get; set; }
        public int? TempoPontoExterno { get; set; }
        public int? TempoPontoEstrategico { get; set; }
        public int? IndPeriodoAgendamento { get; set; }

        [InverseProperty(nameof(SladistanciaTempo.CodSlaNavigation))]
        public virtual ICollection<SladistanciaTempo> SladistanciaTempos { get; set; }
    }
}
