using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ITipoIndiceReajusteRepository
    {
        PagedList<TipoIndiceReajuste> ObterPorParametros(TipoIndiceReajusteParameters parameters);
        void Criar(TipoIndiceReajuste tipoCausa);
        void Atualizar(TipoIndiceReajuste tipoCausa);
        void Deletar(int codTipoIndiceReajuste);
        TipoIndiceReajuste ObterPorCodigo(int codigo);
    }
}
