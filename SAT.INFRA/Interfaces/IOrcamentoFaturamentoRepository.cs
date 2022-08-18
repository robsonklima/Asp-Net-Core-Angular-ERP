using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentoFaturamentoRepository
    {
        PagedList<OrcamentoFaturamento> ObterPorParametros(OrcamentoFaturamentoParameters parameters);
        void Criar(OrcamentoFaturamento orcamento);
        void Atualizar(OrcamentoFaturamento orcamento);
        void Deletar(int codOrcamento);
        OrcamentoFaturamento ObterPorCodigo(int codigo);
    }
}
