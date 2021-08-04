using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class StatusServicoListViewModel : Meta
    {
        public IEnumerable<StatusServico> StatusServico { get; set; }
    }
}
