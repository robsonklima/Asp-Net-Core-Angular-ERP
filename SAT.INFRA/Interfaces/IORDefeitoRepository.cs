using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORDefeitoRepository
    {
        PagedList<ORDefeito> ObterPorParametros(ORDefeitoParameters parameters);
        void Criar(ORDefeito orDefeito);
        void Atualizar(ORDefeito orDefeito);
        void Deletar(int codigo);
        ORDefeito ObterPorCodigo(int codigo);
    }
}
