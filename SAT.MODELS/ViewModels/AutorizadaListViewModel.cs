using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class AutorizadaListViewModel : Meta
    {
        public IEnumerable<Autorizada> Autorizadas { get; set; }
    }
}
