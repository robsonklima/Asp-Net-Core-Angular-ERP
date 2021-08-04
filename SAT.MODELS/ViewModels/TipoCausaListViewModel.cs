using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class TipoCausaListViewModel : Meta
    {
        public IEnumerable<TipoCausa> TiposCausa { get; set; }
    }
}
