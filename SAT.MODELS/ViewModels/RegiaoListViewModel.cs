using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class RegiaoListViewModel : Meta
    {
        public IEnumerable<Regiao> Regioes { get; set; }
    }
}
