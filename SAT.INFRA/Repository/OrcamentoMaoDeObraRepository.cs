using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class OrcamentoMaoDeObraRepository : IOrcamentoMaoDeObraRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoMaoDeObraRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(OrcamentoMaoDeObra orcMaoDeObra)
        {
            OrcamentoMaoDeObra p = _context.OrcamentoMaoDeObra.FirstOrDefault(p => p.CodOrc == orcMaoDeObra.CodOrc);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(orcMaoDeObra);
                _context.ChangeTracker.Clear();
                _context.SaveChanges();
            }
        }

        public void Criar(OrcamentoMaoDeObra orcMaoDeObra)
        {
            _context.Add(orcMaoDeObra);
            _context.SaveChanges();
        }

        public void Deletar(int codOrc)
        {
            OrcamentoMaoDeObra orcMaoDeObra = _context.OrcamentoMaoDeObra.FirstOrDefault(p => p.CodOrc == codOrc);

            if (orcMaoDeObra != null)
            {
                _context.OrcamentoMaoDeObra.Remove(orcMaoDeObra);
                _context.SaveChanges();
            }
        }

        public OrcamentoMaoDeObra ObterPorCodigo(int codigo)
        {
            return _context.OrcamentoMaoDeObra
                .FirstOrDefault(p => p.CodOrcMaoObra == codigo);
        }

        public PagedList<OrcamentoMaoDeObra> ObterPorParametros(OrcamentoMaoDeObraParameters parameters)
        {
            var query = _context.OrcamentoMaoDeObra
                .AsQueryable();

            if (parameters.CodOrc != null)
            {
                query = query.Where(p => p.CodOrc == parameters.CodOrc);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<OrcamentoMaoDeObra>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
