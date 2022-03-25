using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IFormaPagamentoRepository
    {
        PagedList<FormaPagamento> ObterPorParametros(FormaPagamentoParameters parameters);
        void Criar(FormaPagamento formaPagamento);
        void Atualizar(FormaPagamento formaPagamento);
        void Deletar(int codCidade);
        FormaPagamento ObterPorCodigo(int codigo);
    }
}
