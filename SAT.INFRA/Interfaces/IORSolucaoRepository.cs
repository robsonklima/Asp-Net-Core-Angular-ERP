using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORSolucaoRepository
    {
        PagedList<ORSolucao> ObterPorParametros(ORSolucaoParameters parameters);
        ORSolucao Criar(ORSolucao orSolucao);
        void Atualizar(ORSolucao orSolucao);
        void Deletar(int codigo);
        ORSolucao ObterPorCodigo(int codigo);
    }
}
