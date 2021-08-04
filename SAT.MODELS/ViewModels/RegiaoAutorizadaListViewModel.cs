using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class RegiaoAutorizadaListViewModel : Meta
    {
        public IEnumerable<RegiaoAutorizada> RegioesAutorizadas { get; set; }
    }
}
