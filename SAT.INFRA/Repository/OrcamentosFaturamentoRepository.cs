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
    public class OrcamentosFaturamentoRepository : IOrcamentosFaturamentoRepository
    {
        private readonly AppDbContext _context;

        public OrcamentosFaturamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OrcamentosFaturamento orcamentosFaturamento)
        {
            _context.ChangeTracker.Clear();
            OrcamentosFaturamento p = _context.OrcamentosFaturamento.FirstOrDefault(p => p.CodOrcamentoFaturamento == orcamentosFaturamento.CodOrcamentoFaturamento);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(orcamentosFaturamento);
                _context.SaveChanges();
            }
        }

        public void Criar(OrcamentosFaturamento orcamentosFaturamento)
        {
            _context.Add(orcamentosFaturamento);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            OrcamentosFaturamento orcamentosFaturamento = _context
                                                            .OrcamentosFaturamento
                                                                .FirstOrDefault(p => p.CodOrcamentoFaturamento == codigo);

            if (orcamentosFaturamento != null)
            {
                _context.OrcamentosFaturamento.Remove(orcamentosFaturamento);
                _context.SaveChanges();
            }
        }

        public OrcamentosFaturamento ObterPorCodigo(int codigo)
        {
            return _context.OrcamentosFaturamento
                            .FirstOrDefault(p => p.CodOrcamentoFaturamento == codigo);
        }

        public PagedList<OrcamentosFaturamento> ObterPorParametros(OrcamentosFaturamentoParameters parameters)
        {
            var query = _context.OrcamentosFaturamento.AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodOrcamento.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.NumOrcamento.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OrcamentosFaturamento>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
