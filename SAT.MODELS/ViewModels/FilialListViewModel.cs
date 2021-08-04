using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class FilialListViewModel : Meta
    {
        public IEnumerable<Filial> Filiais { get; set; }
    }
}
