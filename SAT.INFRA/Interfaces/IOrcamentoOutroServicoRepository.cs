using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentoOutroServicoRepository
    {
        PagedList<OrcamentoOutroServico> ObterPorParametros(OrcamentoOutroServicoParameters parameters);
        void Criar(OrcamentoOutroServico orcamento);
        void Atualizar(OrcamentoOutroServico orcamento);
        void Deletar(int codOrcamento);
        OrcamentoOutroServico ObterPorCodigo(int codigo);
    }
}
