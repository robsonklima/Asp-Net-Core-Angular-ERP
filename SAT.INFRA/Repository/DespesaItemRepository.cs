using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;
using SAT.MODELS.Entities.Constants;
using Microsoft.EntityFrameworkCore;

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
            _context.ChangeTracker.Clear();
            DespesaItem d = _context.DespesaItem.FirstOrDefault(l => l.CodDespesaItem == despesaItem.CodDespesaItem);

            if (d != null)
            {
                try
                {
                    _context.Entry(d).CurrentValues.SetValues(despesaItem);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Criar(DespesaItem despesaItem)
        {
            _context.Add(despesaItem);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            DespesaItem di = _context.DespesaItem.SingleOrDefault(p => p.CodDespesaItem == codigo);

            if (di != null)
            {
                _context.DespesaItem.Remove(di);

                try
                {
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
                }
            }
        }

        public DespesaItem ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DespesaItem> ObterPorParametros(DespesaItemParameters parameters)
        {
            var despesaItens = _context.DespesaItem.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.CodDespesa))
            {
                var codigos = parameters.CodDespesa.Split(",").Select(i => i.Trim()).Distinct();
                despesaItens = despesaItens.Where(t => codigos.Any(a => a == t.CodDespesa.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesaItens = despesaItens.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaItem>.ToPagedList(despesaItens, parameters.PageNumber, parameters.PageSize);
        }
    }
}