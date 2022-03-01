using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Interfaces
{
    public interface IOrdemServicoRepository
    {
        PagedList<OrdemServico> ObterPorParametros(OrdemServicoParameters parameters);
        IQueryable<OrdemServico> ObterQuery(OrdemServicoParameters parameters);
        void Criar(OrdemServico ordemServico);
        void Atualizar(OrdemServico ordemServico);
        void Deletar(int codOS);
        OrdemServico ObterPorCodigo(int codigo);
    }
}
