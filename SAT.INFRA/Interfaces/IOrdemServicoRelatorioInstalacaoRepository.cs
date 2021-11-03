using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrdemServicoRelatorioInstalacaoRepository
    {
        PagedList<OrdemServicoRelatorioInstalacao> ObterPorParametros(OrdemServicoRelatorioInstalacaoParameters parameters);
        void Criar(OrdemServicoRelatorioInstalacao relatorioInstalacao);
        void Atualizar(OrdemServicoRelatorioInstalacao relatorioInstalacao);
        void Deletar(int codRAT);
        OrdemServicoRelatorioInstalacao ObterPorCodigo(int codigo);
    }
}
