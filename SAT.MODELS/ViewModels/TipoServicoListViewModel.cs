using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class TipoServicoListViewModel : Meta
    {
        public IEnumerable<TipoServico> TiposServico { get; set; }
    }
}
