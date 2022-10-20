using SAT.MODELS.Entities;

namespace SAT.INFRA.Interfaces
{
    public interface IUsuarioLoginRepository
    {
        UsuarioLogin Criar(UsuarioLogin login);
    }
}
