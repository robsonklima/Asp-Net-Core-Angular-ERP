using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Views;

namespace SAT.INFRA.Interfaces
{
    public interface IOrdemServicoRepository
    {
        PagedList<OrdemServico> ObterPorParametros(OrdemServicoParameters parameters);
        IQueryable<OrdemServico> ObterQuery(OrdemServicoParameters parameters);
        OrdemServico Criar(OrdemServico ordemServico);
        void Atualizar(OrdemServico ordemServico);
        void Deletar(int codOS);
        OrdemServico ObterPorCodigo(int codigo);
        List<ViewExportacaoChamadosUnificado> ObterViewPorOs(int[] osList);
    }
}
