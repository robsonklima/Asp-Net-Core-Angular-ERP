using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class EquipamentoListViewModel : Meta
    {
        public IEnumerable<Equipamento> Equipamentos { get; set; }
    }
}
