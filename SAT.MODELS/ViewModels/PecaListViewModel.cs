using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class PecaListViewModel : Meta
    {
        public IEnumerable<Peca> Pecas { get; set; }
    }
}
