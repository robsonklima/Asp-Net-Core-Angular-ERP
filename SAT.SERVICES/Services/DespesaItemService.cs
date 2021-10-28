using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DespesaItemService : IDespesaItemService
    {
        private readonly IDespesaItemRepository _despesaItemRepo;

        public DespesaItemService(IDespesaItemRepository despesaItemRepo)
        {
            _despesaItemRepo = despesaItemRepo;
        }

        public void Atualizar(DespesaItem despesaItem)
        {
            _despesaItemRepo.Atualizar(despesaItem);
        }

        public DespesaItem Criar(DespesaItem despesaItem)
        {
            _despesaItemRepo.Criar(despesaItem);

            return despesaItem;
        }

        public void Deletar(int codigo)
        {
            _despesaItemRepo.Deletar(codigo);
        }

        public DespesaItem ObterPorCodigo(int codigo)
        {
            return _despesaItemRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(DespesaItemParameters parameters)
        {
            var despesaItens = _despesaItemRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = despesaItens,
                TotalCount = despesaItens.TotalCount,
                CurrentPage = despesaItens.CurrentPage,
                PageSize = despesaItens.PageSize,
                TotalPages = despesaItens.TotalPages,
                HasNext = despesaItens.HasNext,
                HasPrevious = despesaItens.HasPrevious
            };

            return lista;
        }
    }
}