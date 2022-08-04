using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentosFaturamentoRepository
    {
        PagedList<OrcamentosFaturamento> ObterPorParametros(OrcamentosFaturamentoParameters parameters);
        void Criar(OrcamentosFaturamento orcamento);
        void Atualizar(OrcamentosFaturamento orcamento);
        void Deletar(int codOrcamento);
        OrcamentosFaturamento ObterPorCodigo(int codigo);
    }
}
