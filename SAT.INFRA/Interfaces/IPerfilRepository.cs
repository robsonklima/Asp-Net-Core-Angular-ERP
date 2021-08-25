using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPerfilRepository
    {
        PagedList<Perfil> ObterPorParametros(PerfilParameters parameters);
        void Criar(Perfil perfil);
        void Deletar(int codigo);
        void Atualizar(Perfil perfil);
        Perfil ObterPorCodigo(int codigo);
    }
}
