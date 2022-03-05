
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface IPlantaoTecnicoClienteRepository
    {
        PagedList<PlantaoTecnicoCliente> ObterPorParametros(PlantaoTecnicoClienteParameters parameters);
        void Criar(PlantaoTecnicoCliente cliente);
        void Deletar(int codigo);
        void Atualizar(PlantaoTecnicoCliente cliente);
        PlantaoTecnicoCliente ObterPorCodigo(int codigo);
    }
}
