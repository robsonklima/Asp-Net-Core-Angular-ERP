using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PlantaoTecnicoService : IPlantaoTecnicoService
    {
        private readonly IPlantaoTecnicoRepository _plantaoTecnicoRepo;

        public PlantaoTecnicoService(IPlantaoTecnicoRepository plantaoTecnicoRepo)
        {
            _plantaoTecnicoRepo = plantaoTecnicoRepo;
        }

        public ListViewModel ObterPorParametros(PlantaoTecnicoParameters parameters)
        {
            var perfis = _plantaoTecnicoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = perfis,
                TotalCount = perfis.TotalCount,
                CurrentPage = perfis.CurrentPage,
                PageSize = perfis.PageSize,
                TotalPages = perfis.TotalPages,
                HasNext = perfis.HasNext,
                HasPrevious = perfis.HasPrevious
            };

            return lista;
        }

        public PlantaoTecnico Criar(PlantaoTecnico plantaoTecnico)
        {
            _plantaoTecnicoRepo.Criar(plantaoTecnico);
            return plantaoTecnico;
        }

        public void Deletar(int codigo)
        {
            _plantaoTecnicoRepo.Deletar(codigo);
        }

        public void Atualizar(PlantaoTecnico plantaoTecnico)
        {
            _plantaoTecnicoRepo.Atualizar(plantaoTecnico);
        }

        public PlantaoTecnico ObterPorCodigo(int codigo)
        {
            return _plantaoTecnicoRepo.ObterPorCodigo(codigo);
        }
    }
}
