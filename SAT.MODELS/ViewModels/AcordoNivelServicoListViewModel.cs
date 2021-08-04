using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class AcordoNivelServicoListViewModel : Meta
    {
        public IEnumerable<AcordoNivelServico> AcordosNivelServico { get; set; }
    }
}