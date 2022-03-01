using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaAdiantamentoService : IDespesaAdiantamentoService
    {
        private readonly IDespesaAdiantamentoRepository _despesaAdiantamentoRepo;

        public DespesaAdiantamentoService(IDespesaAdiantamentoRepository despesaAdiantamentoRepo)
        {
            _despesaAdiantamentoRepo = despesaAdiantamentoRepo;
        }

        public void Atualizar(DespesaAdiantamento despesa)
        {
            _despesaAdiantamentoRepo.Atualizar(despesa);
        }

        public DespesaAdiantamento Criar(DespesaAdiantamento despesa)
        {
            _despesaAdiantamentoRepo.Criar(despesa);

            return despesa;
        }

        public void Deletar(int codigo)
        {
            _despesaAdiantamentoRepo.Deletar(codigo);
        }

        public DespesaAdiantamento ObterPorCodigo(int codigo)
        {
            return _despesaAdiantamentoRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaAdiantamentoParameters parameters)
        {
            var despesaAdiantamento = _despesaAdiantamentoRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = despesaAdiantamento,
                TotalCount = despesaAdiantamento.TotalCount,
                CurrentPage = despesaAdiantamento.CurrentPage,
                PageSize = despesaAdiantamento.PageSize,
                TotalPages = despesaAdiantamento.TotalPages,
                HasNext = despesaAdiantamento.HasNext,
                HasPrevious = despesaAdiantamento.HasPrevious
            };

            return lista;
        }
    }
}