using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class DefeitoListViewModel : Meta
    {
        public IEnumerable<Defeito> Defeitos { get; set; }
    }
}
