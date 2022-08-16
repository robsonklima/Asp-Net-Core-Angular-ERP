using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentoFaturamentoService
    {
        ListViewModel ObterPorParametros(OrcamentoFaturamentoParameters parameters);
        OrcamentoFaturamento Criar(OrcamentoFaturamento orcamento);
        void Deletar(int codigo);
        OrcamentoFaturamento Atualizar(OrcamentoFaturamento orcamento);
        OrcamentoFaturamento ObterPorCodigo(int codigo);
    }
}
