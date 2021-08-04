using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class PerfilListViewModel : Meta
    {
        public IEnumerable<Perfil> Perfis { get; set; }
    }
}
