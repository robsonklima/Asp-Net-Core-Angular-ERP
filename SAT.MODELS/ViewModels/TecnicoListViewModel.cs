using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class TecnicoListViewModel : Meta
    {
        public IEnumerable<Tecnico> Tecnicos { get; set; }
    }
}
