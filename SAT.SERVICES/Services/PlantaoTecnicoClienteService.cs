using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PlantaoTecnicoClienteService : IPlantaoTecnicoClienteService
    {
        private readonly IPlantaoTecnicoClienteRepository _plantaoTecnicoClienteRepo;

        public PlantaoTecnicoClienteService(IPlantaoTecnicoClienteRepository plantaoTecnicoClienteRepo)
        {
            _plantaoTecnicoClienteRepo = plantaoTecnicoClienteRepo;
        }

        public ListViewModel ObterPorParametros(PlantaoTecnicoClienteParameters parameters)
        {
            var perfis = _plantaoTecnicoClienteRepo.ObterPorParametros(parameters);

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

        public PlantaoTecnicoCliente Criar(PlantaoTecnicoCliente plantaoTecnicoCliente)
        {
            _plantaoTecnicoClienteRepo.Criar(plantaoTecnicoCliente);
            return plantaoTecnicoCliente;
        }

        public void Deletar(int codigo)
        {
            _plantaoTecnicoClienteRepo.Deletar(codigo);
        }

        public void Atualizar(PlantaoTecnicoCliente plantaoTecnicoCliente)
        {
            _plantaoTecnicoClienteRepo.Atualizar(plantaoTecnicoCliente);
        }

        public PlantaoTecnicoCliente ObterPorCodigo(int codigo)
        {
            return _plantaoTecnicoClienteRepo.ObterPorCodigo(codigo);
        }
    }
}
