using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class VersaoRepository : IVersaoRepository
    {
        private readonly AppDbContext _context;

        public VersaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Versao versao)
        {
            _context.ChangeTracker.Clear();
            Versao t = _context.Versao.FirstOrDefault(t => t.CodSatVersao == versao.CodSatVersao);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(versao);
                _context.SaveChanges();
            }
        }

        public void Criar(Versao versao)
        {
            _context.Add(versao);
            _context.SaveChanges();
        }

        public void Deletar(int codVersao)
        {
            Versao t = _context.Versao.FirstOrDefault(t => t.CodSatVersao == codVersao);

            if (t != null)
            {
                _context.Versao.Remove(t);
                _context.SaveChanges();
            }
        }

        public Versao ObterPorCodigo(int codigo)
        {
            return _context.Versao.FirstOrDefault(t => t.CodSatVersao == codigo);
        }

        public PagedList<Versao> ObterPorParametros(VersaoParameters parameters)
        {
            var query = _context.Versao
                .Include(v => v.Alteracoes)
                    .ThenInclude(a => a.Tipo)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    t =>
                    t.Nome.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.CodSatVersao.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Versao>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
