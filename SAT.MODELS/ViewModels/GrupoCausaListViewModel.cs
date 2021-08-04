using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class GrupoCausaListViewModel : Meta
    {
        public IEnumerable<GrupoCausa> GruposCausa { get; set; }
    }
}
