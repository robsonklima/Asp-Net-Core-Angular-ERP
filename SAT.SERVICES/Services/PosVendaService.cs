using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class PosVendaService : IPosVendaService
    {
        private readonly IPosVendaRepository _posVendasRepo;

        public PosVendaService(IPosVendaRepository posVendaRepo)
        {
            _posVendasRepo = posVendaRepo;
        }

        public ListViewModel ObterPorParametros(PosVendaParameters parameters)
        {
            var posVendas = _posVendasRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = posVendas,
                TotalCount = posVendas.TotalCount,
                CurrentPage = posVendas.CurrentPage,
                PageSize = posVendas.PageSize,
                TotalPages = posVendas.TotalPages,
                HasNext = posVendas.HasNext,
                HasPrevious = posVendas.HasPrevious
            };

            return lista;
        }

        public PosVenda ObterPorCodigo(int codigo)
        {
            return _posVendasRepo.ObterPorCodigo(codigo);
        }
    }
}
