using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PlantaoTecnicoRegiaoService : IPlantaoTecnicoRegiaoService
    {
        private readonly IPlantaoTecnicoRegiaoRepository _plantaoTecnicoRegiaoRepo;

        public PlantaoTecnicoRegiaoService(IPlantaoTecnicoRegiaoRepository plantaoTecnicoRegiaoRepo)
        {
            _plantaoTecnicoRegiaoRepo = plantaoTecnicoRegiaoRepo;
        }

        public ListViewModel ObterPorParametros(PlantaoTecnicoRegiaoParameters parameters)
        {
            var perfis = _plantaoTecnicoRegiaoRepo.ObterPorParametros(parameters);

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

        public PlantaoTecnicoRegiao Criar(PlantaoTecnicoRegiao plantaoTecnicoRegiao)
        {
            _plantaoTecnicoRegiaoRepo.Criar(plantaoTecnicoRegiao);
            return plantaoTecnicoRegiao;
        }

        public void Deletar(int codigo)
        {
            _plantaoTecnicoRegiaoRepo.Deletar(codigo);
        }

        public void Atualizar(PlantaoTecnicoRegiao plantaoTecnicoRegiao)
        {
            _plantaoTecnicoRegiaoRepo.Atualizar(plantaoTecnicoRegiao);
        }

        public PlantaoTecnicoRegiao ObterPorCodigo(int codigo)
        {
            return _plantaoTecnicoRegiaoRepo.ObterPorCodigo(codigo);
        }
    }
}
