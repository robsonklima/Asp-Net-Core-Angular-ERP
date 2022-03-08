using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class FormaPagamentoService : IFormaPagamentoService
    {
        private readonly IFormaPagamentoRepository _formaPagamentoRepo;

        public FormaPagamentoService(IFormaPagamentoRepository formaPagamentoRepo)
        {
            _formaPagamentoRepo = formaPagamentoRepo;
        }

        public ListViewModel ObterPorParametros(FormaPagamentoParameters parameters)
        {
            var formaPagamentos = _formaPagamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = formaPagamentos,
                TotalCount = formaPagamentos.TotalCount,
                CurrentPage = formaPagamentos.CurrentPage,
                PageSize = formaPagamentos.PageSize,
                TotalPages = formaPagamentos.TotalPages,
                HasNext = formaPagamentos.HasNext,
                HasPrevious = formaPagamentos.HasPrevious
            };

            return lista;
        }
    }
}
