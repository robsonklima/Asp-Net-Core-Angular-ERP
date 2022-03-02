using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository _cargoRepo;

        public CargoService(ICargoRepository cargoRepo)
        {
            _cargoRepo = cargoRepo;
        }

        public ListViewModel ObterPorParametros(CargoParameters parameters)
        {
            var cargos = _cargoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = cargos,
                TotalCount = cargos.TotalCount,
                CurrentPage = cargos.CurrentPage,
                PageSize = cargos.PageSize,
                TotalPages = cargos.TotalPages,
                HasNext = cargos.HasNext,
                HasPrevious = cargos.HasPrevious
            };

            return lista;
        }

        public Cargo Criar(Cargo Cargo)
        {
            _cargoRepo.Criar(Cargo);
            return Cargo;
        }

        public void Deletar(int codigo)
        {
            _cargoRepo.Deletar(codigo);
        }

        public void Atualizar(Cargo Cargo)
        {
            _cargoRepo.Atualizar(Cargo);
        }

        public Cargo ObterPorCodigo(int codigo)
        {
            return _cargoRepo.ObterPorCodigo(codigo);
        }

        public Cargo BuscaCargoPorNome(string nomeCargo)
        {
            return _cargoRepo.BuscaCargoPorNome(nomeCargo);            
        }
    }
}
