using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class RelatorioAtendimentoListViewModel : Meta
    {
        public IEnumerable<RelatorioAtendimento> RelatoriosAtendimento { get; set; }
    }
}
