using System;
using SAT.MODELS.Enums;

namespace SAT.MODELS.ViewModels
{
    public class ViewAgendaTecnicoEvento
    {
        public int? CodAgendaTecnico { get; set; }
        public string Cor { get; set; }
        public string Titulo { get; set; }
        public bool Editavel { get; set; }
        public AgendaTecnicoTipoEnum Tipo { get; set; }
        public int CodFilial { get; set; }
        public string CodUsuario { get; set; }
        public int CodTecnico { get; set; }
        public int? CodOS { get; set; }
        public int? CodStatusServico { get; set; }
        public string NomeStatusServico { get; set; }
        public string NomTipoIntervencao { get; set; }
        public string NomeLocal { get; set; }
        public string Clientes { get; set; }
        public DateTime? Inicio { get; set; }
        public DateTime? Fim { get; set; }
        public DateTime? Data { get; set; }
        public DateTime? DataHoraLimiteAtendimento { get; set; }
        public DateTime? DataAgendamento { get; set; }
        public DateTime? DataHoraCad { get; set; }
        public string CodUsuarioCad { get; set; }
        public int? IndAtivo { get; set; }
        public DateTime? InicioAtendimento { get; set; }
        public DateTime? FimAtendimento { get; set; }
    }
}
