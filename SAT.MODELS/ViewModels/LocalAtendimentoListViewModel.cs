using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class LocalAtendimentoListViewModel : Meta
    {
        public IEnumerable<LocalAtendimento> LocaisAtendimento { get; set; }
    }
}
