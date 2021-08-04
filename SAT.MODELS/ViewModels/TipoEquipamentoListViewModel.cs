using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class TipoEquipamentoListViewModel : Meta
    {
        public IEnumerable<TipoEquipamento> TiposEquipamento { get; set; }
    }
}
