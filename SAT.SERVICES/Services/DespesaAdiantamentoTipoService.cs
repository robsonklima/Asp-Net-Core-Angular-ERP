using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaAdiantamentoTipoService : IDespesaAdiantamentoTipoService
    {
        private readonly IDespesaAdiantamentoTipoRepository _adiantamentoTipoRepo;

        public DespesaAdiantamentoTipoService(IDespesaAdiantamentoTipoRepository adiantamentoTipoRepo)
        {
            _adiantamentoTipoRepo = adiantamentoTipoRepo;
        }

        public ListViewModel ObterPorParametros(DespesaAdiantamentoTipoParameters parameters)
        {
            var tipos = _adiantamentoTipoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = tipos,
                TotalCount = tipos.TotalCount,
                CurrentPage = tipos.CurrentPage,
                PageSize = tipos.PageSize,
                TotalPages = tipos.TotalPages,
                HasNext = tipos.HasNext,
                HasPrevious = tipos.HasPrevious
            };

            return lista;
        }
    }
}