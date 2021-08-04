using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.API.Repositories.Interfaces
{
    public interface ITipoCausaRepository
    {
        PagedList<TipoCausa> ObterPorParametros(TipoCausaParameters parameters);
        void Criar(TipoCausa tipoCausa);
        void Atualizar(TipoCausa tipoCausa);
        void Deletar(int codTipoCausa);
        TipoCausa ObterPorCodigo(int codigo);
    }
}
