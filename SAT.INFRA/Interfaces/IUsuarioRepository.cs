using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Login(Usuario usuario);
        PagedList<Usuario> ObterPorParametros(UsuarioParameters parameters);
        Usuario ObterPorCodigo(string codigo);
        void Atualizar(Usuario usuario);
    }
}
