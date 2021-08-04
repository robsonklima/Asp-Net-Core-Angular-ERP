using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class GrupoEquipamentoListViewModel : Meta
    {
        public IEnumerable<GrupoEquipamento> GruposEquipamento { get; set; }
    }
}
