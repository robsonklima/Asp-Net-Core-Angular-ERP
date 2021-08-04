using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.MODELS.ViewModels
{
    public class UsuarioListViewModel : Meta
    {
        public IEnumerable<Usuario> Usuarios { get; set; }
    }
}
