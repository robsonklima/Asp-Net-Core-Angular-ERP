using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class TransportadoraListViewModel : Meta
    {
        public IEnumerable<Transportadora> Transportadoras { get; set; }
    }
}
