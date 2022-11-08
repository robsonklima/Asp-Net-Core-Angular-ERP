using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IORSolucaoService
    {
        ListViewModel ObterPorParametros(ORSolucaoParameters parameters);
        ORSolucao Criar(ORSolucao orSolucao);
        void Deletar(int codigo);
        void Atualizar(ORSolucao orSolucao);
        ORSolucao ObterPorCodigo(int codigo);
    }
}
