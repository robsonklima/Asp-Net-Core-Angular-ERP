using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
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
