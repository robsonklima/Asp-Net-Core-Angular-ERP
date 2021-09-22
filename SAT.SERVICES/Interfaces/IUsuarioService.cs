using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioLoginViewModel Login(Usuario usuario);
        ListViewModel ObterPorParametros(UsuarioParameters parameters);
        Usuario ObterPorCodigo(string codigo);
    }
}
