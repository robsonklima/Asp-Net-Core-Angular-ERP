using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentosFaturamentoService
    {
        ListViewModel ObterPorParametros(OrcamentosFaturamentoParameters parameters);
        OrcamentosFaturamento Criar(OrcamentosFaturamento orcamento);
        void Deletar(int codigo);
        OrcamentosFaturamento Atualizar(OrcamentosFaturamento orcamento);
        OrcamentosFaturamento ObterPorCodigo(int codigo);
    }
}
