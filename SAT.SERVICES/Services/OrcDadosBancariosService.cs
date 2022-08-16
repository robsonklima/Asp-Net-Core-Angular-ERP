using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services {
    public class OrcDadosBancariosService : IOrcDadosBancariosService
    {
        private readonly IOrcDadosBancariosRepository _formaPagamentoRepo;

        public OrcDadosBancariosService(IOrcDadosBancariosRepository formaPagamentoRepo)
        {
            _formaPagamentoRepo = formaPagamentoRepo;
        }

        public ListViewModel ObterPorParametros(OrcDadosBancariosParameters parameters)
        {
            var formasPagamento = _formaPagamentoRepo.ObterPorParametros(parameters);

            var listViewModel = new ListViewModel {
                Items = formasPagamento,
                CurrentPage = formasPagamento.CurrentPage,
                PageSize = formasPagamento.PageSize,
                HasNext = formasPagamento.HasNext,
                HasPrevious = formasPagamento.HasPrevious,
                TotalCount = formasPagamento.TotalCount,
                TotalPages = formasPagamento.TotalPages
            };

            return listViewModel;
        }
    }
}