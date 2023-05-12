using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ITipoComunicacaoRepository
    {
        PagedList<TipoComunicacao> ObterPorParametros(TipoComunicacaoParameters parameters);
        TipoComunicacao Criar(TipoComunicacao tipo);
        TipoComunicacao Atualizar(TipoComunicacao tipo);
        TipoComunicacao Deletar(int cod);
        TipoComunicacao ObterPorCodigo(int cod);
    }
}
