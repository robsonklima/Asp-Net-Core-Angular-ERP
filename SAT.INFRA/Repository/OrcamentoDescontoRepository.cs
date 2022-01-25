using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class OrcamentoDescontoRepository : IOrcamentoDescontoRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoDescontoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OrcamentoDesconto orcamentoDesconto)
        {
            _context.ChangeTracker.Clear();
            OrcamentoDesconto p = _context.OrcamentoDesconto.FirstOrDefault(p => p.CodOrcDesconto == orcamentoDesconto.CodOrcDesconto);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(orcamentoDesconto);
                _context.SaveChanges();
            }
        }

        public void Criar(OrcamentoDesconto orcamentoDesconto)
        {
            _context.Add(orcamentoDesconto);
            _context.SaveChanges();
        }

        public void Deletar(int codOrcDesconto)
        {
            OrcamentoDesconto orcamento = _context.OrcamentoDesconto.FirstOrDefault(p => p.CodOrcDesconto == codOrcDesconto);

            if (orcamento != null)
            {
                _context.OrcamentoDesconto.Remove(orcamento);
                _context.SaveChanges();
            }
        }

        public OrcamentoDesconto ObterPorCodigo(int codigo)
        {
            return _context.OrcamentoDesconto
                .FirstOrDefault(p => p.CodOrc == codigo);
        }

        public PagedList<OrcamentoDesconto> ObterPorParametros(OrcamentoDescontoParameters parameters)
        {
            var query = _context.OrcamentoDesconto
                .AsQueryable();

            if (parameters.CodOrc != null)
            {
                query = query.Where(orc => orc.CodOrc == parameters.CodOrc);
            }

            if (parameters.NomeTipo != null)
            {
                query = query.Where(orc => orc.NomeTipo.Contains(parameters.NomeTipo));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<OrcamentoDesconto>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
