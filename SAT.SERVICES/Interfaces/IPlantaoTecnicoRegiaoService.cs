using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IPlantaoTecnicoRegiaoService
    {
        ListViewModel ObterPorParametros(PlantaoTecnicoRegiaoParameters parameters);
        PlantaoTecnicoRegiao Criar(PlantaoTecnicoRegiao regiao);
        void Deletar(int codigo);
        void Atualizar(PlantaoTecnicoRegiao regiao);
        PlantaoTecnicoRegiao ObterPorCodigo(int codigo);
    }
}
