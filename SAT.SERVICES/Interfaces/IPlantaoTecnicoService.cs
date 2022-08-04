using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPlantaoTecnicoService
    {
        ListViewModel ObterPorParametros(PlantaoTecnicoParameters parameters);
        PlantaoTecnico Criar(PlantaoTecnico plantao);
        void Deletar(int codigo);
        void Atualizar(PlantaoTecnico plantao);
        PlantaoTecnico ObterPorCodigo(int codigo);
        void ProcessarTaskEmailsSobreaviso();
    }
}
