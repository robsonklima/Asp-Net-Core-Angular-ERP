using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IORItemInsumoService
    {
        ListViewModel ObterPorParametros(ORItemInsumoParameters parameters);
        ORItemInsumo Criar(ORItemInsumo insumo);
        void Deletar(int codigo);
        ORItemInsumo Atualizar(ORItemInsumo insumo);
        ORItemInsumo ObterPorCodigo(int codigo);
    }
}
