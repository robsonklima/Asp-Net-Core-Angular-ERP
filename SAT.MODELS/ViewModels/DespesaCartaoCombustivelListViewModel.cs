using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class DespesaCartaoCombustivelListViewModel : Meta
    {
        public IEnumerable<DespesaCartaoCombustivel> DespesaCartoesCombustivel { get; set; }
    }
}
