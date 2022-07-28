using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class FormaPagamentoRepository : IFormaPagamentoRepository
    {
        private readonly AppDbContext _context;

        public FormaPagamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(FormaPagamento formaPagamento)
        {
            _context.ChangeTracker.Clear();
            FormaPagamento c = _context.FormaPagamento.FirstOrDefault(c => c.CodFormaPagto == formaPagamento.CodFormaPagto);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(formaPagamento);
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Criar(FormaPagamento formaPagamento)
        {
            _context.Add(formaPagamento);
            _context.SaveChanges();
        }

        public void Deletar(int codFormaPagamento)
        {
            FormaPagamento c = _context.FormaPagamento.FirstOrDefault(c => c.CodFormaPagto == codFormaPagamento);

            if (c != null)
            {
                _context.FormaPagamento.Remove(c);
                _context.SaveChanges();
            }
        }

        public FormaPagamento ObterPorCodigo(int codigo)
        {
            return _context.FormaPagamento.FirstOrDefault(c => c.CodFormaPagto == codigo);
        }

        public PagedList<FormaPagamento> ObterPorParametros(FormaPagamentoParameters parameters)
        {
           // IQueryable<FormaPagamento> formasPagamento = _context.FormaPagamento.AsQueryable();
            var formasPagamento = _context.FormaPagamento
                .DefaultIfEmpty()
                .AsQueryable();

            if (parameters.Filter != null)
            {
                formasPagamento = formasPagamento.Where(
                            f =>
                            f.CodFormaPagto.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            f.DescFormaPagto.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

             if (!string.IsNullOrWhiteSpace(parameters.CodFormasPagto))
            {
                int[] cods = parameters.CodFormasPagto.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                formasPagamento = formasPagamento.Where(dc => cods.Contains(dc.CodFormaPagto));
            }

            if (parameters.IndAtivo != null)
            {
                formasPagamento = formasPagamento.Where(p => p.IndAtivo == parameters.IndAtivo.Value);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                formasPagamento = formasPagamento.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<FormaPagamento>.ToPagedList(formasPagamento, parameters.PageNumber, parameters.PageSize);
        }
    }
}
