using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAT.MODELS.Entities
{
    public class AcordoNivelServicoLegado
    {
        public int CodSla { get; set; }
        public string NomeSla { get; set; }
        public string DescSla { get; set; }
        public float? TempoInicio { get; set; }
        public float? TempoReparo { get; set; }
        public float? TempoSolucao { get; set; }
        public DateTime? HorarioInicio { get; set; }
        public DateTime? HorarioFim { get; set; }
        public int? Kmhora { get; set; }
        public int? KmhoraExtra { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodUsuarioCadastro { get; set; }
        public DateTime? DataManutencao { get; set; }
        public string CodUsuarioManutencao { get; set; }
        public byte? IndAgendamento { get; set; }
        public byte? IndSabado { get; set; }
        public byte? IndDomingo { get; set; }
        public byte? IndFeriado { get; set; }
    }
}

