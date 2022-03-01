using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ITipoContratoRepository
    {
        PagedList<TipoContrato> ObterPorParametros(TipoContratoParameters parameters);
        void Criar(TipoContrato tipoCausa);
        void Atualizar(TipoContrato tipoCausa);
        void Deletar(int codTipoContrato);
        TipoContrato ObterPorCodigo(int codigo);
    }
}
