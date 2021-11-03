using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrdemServicoRelatorioInstalacaoService
    {
        ListViewModel ObterPorParametros(OrdemServicoRelatorioInstalacaoParameters parameters);
        OrdemServicoRelatorioInstalacao Criar(OrdemServicoRelatorioInstalacao relatorioInstalacao);
        void Deletar(int codigo);
        OrdemServicoRelatorioInstalacao Atualizar(OrdemServicoRelatorioInstalacao relatorioInstalacao);
        OrdemServicoRelatorioInstalacao ObterPorCodigo(int codigo);
    }
}
