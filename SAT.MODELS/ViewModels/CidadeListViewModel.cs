using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class CidadeListViewModel : Meta
    {
        public IEnumerable<Cidade> Cidades { get; set; }
    }
}
