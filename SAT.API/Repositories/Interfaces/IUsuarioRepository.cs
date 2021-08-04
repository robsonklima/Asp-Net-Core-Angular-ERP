using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.API.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Login(Usuario usuario);
        PagedList<Usuario> ObterPorParametros(UsuarioParameters parameters);
        Usuario ObterPorCodigo(string codigo);
    }
}
