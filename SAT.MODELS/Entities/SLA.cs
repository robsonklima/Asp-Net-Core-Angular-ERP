using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    [Table("SLA_NEW")]
    public class SLA
    {
        [Key]
        public int CodSLA { get; set; }
        public string NomeSLA { get; set; }
        public string DescSLA { get; set; }
        public DateTime? HorarioInicio { get; set; }
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
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime? DataManutencao { get; set; }
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
    }
}
