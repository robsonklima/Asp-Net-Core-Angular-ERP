using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class MotivoAgendamentoListViewModel : Meta
    {
        public IEnumerable<MotivoAgendamento> MotivosAgendamento { get; set; }
    }
}
