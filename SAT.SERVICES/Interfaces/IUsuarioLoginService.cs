using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IUsuarioLoginService
    {
        ListViewModel ObterPorParametros(UsuarioLoginParameters parameters);
        UsuarioLogin Criar(UsuarioLogin login);
    }
}
