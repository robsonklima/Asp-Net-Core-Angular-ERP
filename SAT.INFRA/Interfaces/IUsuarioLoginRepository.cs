using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IUsuarioLoginRepository
    {
        UsuarioLogin Criar(UsuarioLogin login);
        PagedList<UsuarioLogin> ObterPorParametros(UsuarioLoginParameters parameters);
    }
}
