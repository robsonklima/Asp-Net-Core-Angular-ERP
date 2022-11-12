using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IORItemInsumoRepository
    {
        PagedList<ORItemInsumo> ObterPorParametros(ORItemInsumoParameters parameters);
        ORItemInsumo Criar(ORItemInsumo insumo);
        ORItemInsumo Atualizar(ORItemInsumo insumo);
        void Deletar(int codigo);
        ORItemInsumo ObterPorCodigo(int codigo);
    }
}
