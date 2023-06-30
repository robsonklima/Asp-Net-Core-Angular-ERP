using System;

namespace SAT.MODELS.Entities
{
    public class ANS
    {
        public int CodANS { get; set; }
        public int CodSLA { get; set; }
        public string NomeANS { get; set; }
        public string DescANS { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
        public int TempoHoras { get; set; }
        public string PermiteAgendamento { get; set; }
        public string Sabado { get; set; }
        public string Domingo { get; set; }
        public string Feriado { get; set; }
        public DateTime DataCadastro { get; set; }
        public string CodUsuarioCad { get; set; }
        public string ArredondaHoraFinal { get; set; }
    }
}

