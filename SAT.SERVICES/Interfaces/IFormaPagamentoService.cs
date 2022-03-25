using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IFormaPagamentoService
    {
        ListViewModel ObterPorParametros(FormaPagamentoParameters parameters);
        FormaPagamento Criar(FormaPagamento formaPagamento);
        void Deletar(int codigo);
        void Atualizar(FormaPagamento formaPagamento);
        FormaPagamento ObterPorCodigo(int codigo);
    }
}
