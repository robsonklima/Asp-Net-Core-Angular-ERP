using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITipoComunicacaoService
    {
        ListViewModel ObterPorParametros(TipoComunicacaoParameters parameters);
        TipoComunicacao Criar(TipoComunicacao tipo);
        TipoComunicacao Atualizar(TipoComunicacao tipo);
        TipoComunicacao Deletar(int codigo);
        TipoComunicacao ObterPorCodigo(int codigo);
    }
}
