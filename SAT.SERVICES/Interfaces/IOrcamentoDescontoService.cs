using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IOrcamentoDescontoService
    {
        ListViewModel ObterPorParametros(OrcamentoDescontoParameters parameters);
        OrcamentoDesconto Criar(OrcamentoDesconto orcamento);
        void Deletar(int codigo);
        OrcamentoDesconto Atualizar(OrcamentoDesconto orcamento);
        OrcamentoDesconto ObterPorCodigo(int codigo);
    }
}
