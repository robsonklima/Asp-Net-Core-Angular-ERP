using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class DespesaItemRepository : IDespesaItemRepository
    {
        private readonly AppDbContext _context;

        public DespesaItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaItem despesaItem)
        {
            throw new NotImplementedException();
        }

        public void Criar(DespesaItem despesaItem)
        {
            _context.Add(despesaItem);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaItem ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DespesaItem> ObterPorParametros(DespesaItemParameters parameters)
        {
            var despesaItens = _context.DespesaItem.AsQueryable();

            if (parameters.CodDespesa.HasValue)
                despesaItens = despesaItens.Where(e => e.CodDespesa == parameters.CodDespesa);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesaItens = despesaItens.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaItem>.ToPagedList(despesaItens, parameters.PageNumber, parameters.PageSize);
        }
    }
}