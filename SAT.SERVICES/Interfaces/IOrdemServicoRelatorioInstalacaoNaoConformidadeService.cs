using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrdemServicoRelatorioInstalacaoNaoConformidadeService
    {
        ListViewModel ObterPorParametros(OrdemServicoRelatorioInstalacaoNaoConformidadeParameters parameters);
        OrdemServicoRelatorioInstalacaoNaoConformidade Criar(OrdemServicoRelatorioInstalacaoNaoConformidade relatorioInstalacao);
        void Deletar(int codigo);
        OrdemServicoRelatorioInstalacaoNaoConformidade Atualizar(OrdemServicoRelatorioInstalacaoNaoConformidade relatorioInstalacao);
        OrdemServicoRelatorioInstalacaoNaoConformidade ObterPorCodigo(int codigo);
    }
}
