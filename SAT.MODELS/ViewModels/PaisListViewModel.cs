using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class PaisListViewModel : Meta
    {
        public IEnumerable<Pais> Paises { get; set; }
    }
}
