using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrdemServicoSTNRepository
    {
        PagedList<OrdemServicoSTN> ObterPorParametros(OrdemServicoSTNParameters parameters);
        OrdemServicoSTN Criar(OrdemServicoSTN ordemServicoSTN);
        OrdemServicoSTN Atualizar(OrdemServicoSTN ordemServicoSTN);
        void Deletar(int codOrdemServicoSTN);
        OrdemServicoSTN ObterPorCodigo(int codigo);
    }
}
