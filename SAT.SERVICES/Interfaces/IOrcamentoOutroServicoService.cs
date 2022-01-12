using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentoOutroServicoService
    {
        ListViewModel ObterPorParametros(OrcamentoOutroServicoParameters parameters);
        OrcamentoOutroServico Criar(OrcamentoOutroServico orcamentoOutroServico);
        void Deletar(int codigo);
        OrcamentoOutroServico Atualizar(OrcamentoOutroServico orcamentoOutroServico);
        OrcamentoOutroServico ObterPorCodigo(int codigo);
    }
}
