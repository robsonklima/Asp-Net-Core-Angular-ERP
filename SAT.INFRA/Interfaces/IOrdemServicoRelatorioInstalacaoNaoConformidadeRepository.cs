using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrdemServicoRelatorioInstalacaoNaoConformidadeRepository
    {
        PagedList<OrdemServicoRelatorioInstalacaoNaoConformidade> ObterPorParametros(OrdemServicoRelatorioInstalacaoNaoConformidadeParameters parameters);
        void Criar(OrdemServicoRelatorioInstalacaoNaoConformidade relatorioInstalacao);
        void Atualizar(OrdemServicoRelatorioInstalacaoNaoConformidade relatorioInstalacao);
        void Deletar(int codRAT);
        OrdemServicoRelatorioInstalacaoNaoConformidade ObterPorCodigo(int codigo);
    }
}
