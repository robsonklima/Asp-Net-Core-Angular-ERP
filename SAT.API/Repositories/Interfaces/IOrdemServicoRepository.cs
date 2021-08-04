using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface IOrdemServicoRepository
    {
        PagedList<OrdemServico> ObterPorParametros(OrdemServicoParameters parameters);
        void Criar(OrdemServico ordemServico);
        void Atualizar(OrdemServico ordemServico);
        void Deletar(int codOS);
        OrdemServico ObterPorCodigo(int codigo);
    }
}
