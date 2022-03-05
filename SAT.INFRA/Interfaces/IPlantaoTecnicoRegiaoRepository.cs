using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface IPlantaoTecnicoRegiaoRepository
    {
        PagedList<PlantaoTecnicoRegiao> ObterPorParametros(PlantaoTecnicoRegiaoParameters parameters);
        void Criar(PlantaoTecnicoRegiao regiao);
        void Deletar(int codigo);
        void Atualizar(PlantaoTecnicoRegiao regiao);
        PlantaoTecnicoRegiao ObterPorCodigo(int codigo);
    }
}
