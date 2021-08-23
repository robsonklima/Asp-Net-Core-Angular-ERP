using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class ContratoSLAListViewModel : Meta
    {
        public IEnumerable<ContratoSLA> ContratosSLA { get; set; }
    }
}
