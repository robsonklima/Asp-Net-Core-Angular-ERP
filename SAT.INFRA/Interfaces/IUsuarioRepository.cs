using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Login(Usuario usuario);
        PagedList<Usuario> ObterPorParametros(UsuarioParameters parameters);
        Usuario ObterPorCodigo(string codigo);
    }
}
