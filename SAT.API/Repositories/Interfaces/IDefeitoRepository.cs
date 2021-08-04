using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IDefeitoRepository
    {
        PagedList<Defeito> ObterPorParametros(DefeitoParameters parameters);
        void Criar(Defeito defeito);
        void Atualizar(Defeito defeito);
        void Deletar(int codDefeito);
        Defeito ObterPorCodigo(int codigo);
    }
}
