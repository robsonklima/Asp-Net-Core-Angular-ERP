using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class NavegacaoListViewModel : Meta
    {
        public IEnumerable<Navegacao> Navegacoes { get; set; }
    }
}
