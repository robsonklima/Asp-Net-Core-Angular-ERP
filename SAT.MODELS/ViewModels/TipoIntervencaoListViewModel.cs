using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class TipoIntervencaoListViewModel : Meta
    {
        public IEnumerable<TipoIntervencao> TiposIntervencao { get; set; }
    }
}
