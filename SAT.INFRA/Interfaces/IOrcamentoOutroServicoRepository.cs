using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentoOutroServicoRepository
    {
        PagedList<OrcamentoOutroServico> ObterPorParametros(OrcamentoOutroServicoParameters parameters);
        OrcamentoOutroServico Criar(OrcamentoOutroServico orcamento);
        OrcamentoOutroServico Atualizar(OrcamentoOutroServico orcamento);
        void Deletar(int codOrcamento);
        OrcamentoOutroServico ObterPorCodigo(int codigo);
    }
}
