using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Interfaces
{
    public interface IPlantaoTecnicoRepository
    {
        PagedList<PlantaoTecnico> ObterPorParametros(PlantaoTecnicoParameters parameters);
        void Criar(PlantaoTecnico plantao);
        void Deletar(int codigo);
        void Atualizar(PlantaoTecnico plantao);
        PlantaoTecnico ObterPorCodigo(int codigo);
    }
}
