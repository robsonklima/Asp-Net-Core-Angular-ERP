using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IGrupoCausaRepository
    {
        PagedList<GrupoCausa> ObterPorParametros(GrupoCausaParameters parameters);
        void Criar(GrupoCausa grupoCausa);
        void Atualizar(GrupoCausa grupoCausa);
        void Deletar(int codGrupoCausa);
        GrupoCausa ObterPorCodigo(int codigo);
    }
}
