using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IOrcamentoDescontoRepository
    {
        PagedList<OrcamentoDesconto> ObterPorParametros(OrcamentoDescontoParameters parameters);
        void Criar(OrcamentoDesconto orcamento);
        void Atualizar(OrcamentoDesconto orcamento);
        void Deletar(int codOrcamento);
        OrcamentoDesconto ObterPorCodigo(int codigo);
    }
}
