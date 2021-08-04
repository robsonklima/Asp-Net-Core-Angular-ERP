using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class ContratoListViewModel : Meta
    {
        public IEnumerable<Contrato> Contratos { get; set; }
    }
}
