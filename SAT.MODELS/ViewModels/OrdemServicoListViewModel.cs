using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class OrdemServicoListViewModel : Meta
    {
        public IEnumerable<OrdemServico> OrdensServico { get; set; }
    }
}
