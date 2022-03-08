using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class MoedaService : IMoedaService
    {
        private readonly IMoedaRepository _moedaRepo;

        public MoedaService(IMoedaRepository moedaRepo)
        {
            _moedaRepo = moedaRepo;
        }

        public ListViewModel ObterPorParametros(MoedaParameters parameters)
        {
            var moedas = _moedaRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = moedas,
                TotalCount = moedas.TotalCount,
                CurrentPage = moedas.CurrentPage,
                PageSize = moedas.PageSize,
                TotalPages = moedas.TotalPages,
                HasNext = moedas.HasNext,
                HasPrevious = moedas.HasPrevious
            };

            return lista;
        }
    }
}
