using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IPlantaoTecnicoClienteService
    {
        ListViewModel ObterPorParametros(PlantaoTecnicoClienteParameters parameters);
        PlantaoTecnicoCliente Criar(PlantaoTecnicoCliente cliente);
        void Deletar(int codigo);
        void Atualizar(PlantaoTecnicoCliente cliente);
        PlantaoTecnicoCliente ObterPorCodigo(int codigo);
    }
}
