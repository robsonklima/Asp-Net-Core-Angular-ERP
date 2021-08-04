using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IRegiaoRepository
    {
        PagedList<Regiao> ObterPorParametros(RegiaoParameters parameters);
        void Criar(Regiao regiao);
        void Deletar(int codigo);
        void Atualizar(Regiao regiao);
        Regiao ObterPorCodigo(int codigo);
    }
}
