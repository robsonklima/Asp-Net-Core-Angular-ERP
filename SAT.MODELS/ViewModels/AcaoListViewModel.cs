using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class AcaoListViewModel : Meta
    {
        public IEnumerable<Acao> Acoes { get; set; }
    }
}
