using System;
using SAT.MODELS.Enums;

namespace SAT.MODELS.Entities
{
    public class AgendaTecnico
    {
        public int CodAgendaTecnico { get; set; }
        public int? CodTecnico { get; set; }
        public string Titulo { get; set; }
        public string Cor { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public AgendaTecnicoTypeEnum Tipo { get; set; }
        public int IndAgendamento { get; set; }
        public int IndAtivo { get; set; }
        public int? CodOS { get; set; }
        public OrdemServico OrdemServico { get; set; }
        public string CodUsuarioManut { get; set; }
        public DateTime? DataHoraManut { get; set; }
        public string CodUsuarioCad { get; set; }
        public DateTime DataHoraCad { get; set; }
    }

    public class AgendaTecnicoDistanceModel
    {
        public int CodAgendaTecnico { get; set; }
        public double Distancia { get; set; }
    }
}