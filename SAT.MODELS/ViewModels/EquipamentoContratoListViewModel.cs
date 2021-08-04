using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class EquipamentoContratoListViewModel : Meta
    {
        public IEnumerable<EquipamentoContrato> EquipamentosContrato { get; set; }
    }
}
