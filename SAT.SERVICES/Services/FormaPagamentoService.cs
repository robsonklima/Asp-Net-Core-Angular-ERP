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
        private readonly ISequenciaRepository _sequenciaRepo;

        public FormaPagamentoService(IFormaPagamentoRepository formaPagamentoRepo, ISequenciaRepository sequenciaRepo)
        {
            _formaPagamentoRepo = formaPagamentoRepo;
            this._sequenciaRepo = sequenciaRepo;
        }

        public void Atualizar(FormaPagamento formaPagamento)
        {
            this._formaPagamentoRepo.Atualizar(formaPagamento);
        }

        public FormaPagamento Criar(FormaPagamento formaPagamento)
        {
            formaPagamento.CodFormaPagto = this._sequenciaRepo.ObterContador("FormaPagto");
            this._formaPagamentoRepo.Criar(formaPagamento);
            return formaPagamento;
        }

        public void Deletar(int codigo)
        {
            this._formaPagamentoRepo.Deletar(codigo);
        }

        public FormaPagamento ObterPorCodigo(int codigo)
        {
            return this._formaPagamentoRepo.ObterPorCodigo(codigo);
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
