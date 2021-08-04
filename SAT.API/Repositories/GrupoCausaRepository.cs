using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.API.Repositories
{
    public class GrupoCausaRepository : IGrupoCausaRepository
    {
        private readonly AppDbContext _context;

        public GrupoCausaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(GrupoCausa grupoCausa)
        {
            GrupoCausa gc = _context.GrupoCausa.FirstOrDefault(gc => gc.CodGrupoCausa == grupoCausa.CodGrupoCausa);

            if (gc != null)
            {
                _context.Entry(gc).CurrentValues.SetValues(grupoCausa);
                _context.SaveChanges();
            }
        }

        public void Criar(GrupoCausa grupoCausa)
        {
            _context.Add(grupoCausa);
            _context.SaveChanges();
        }

        public void Deletar(int codGrupoCausa)
        {
            GrupoCausa gc = _context.GrupoCausa.FirstOrDefault(gc => gc.CodGrupoCausa == codGrupoCausa);

            if (gc != null)
            {
                _context.GrupoCausa.Remove(gc);
                _context.SaveChanges();
            }
        }

        public GrupoCausa ObterPorCodigo(int codigo)
        {
            return _context.GrupoCausa.FirstOrDefault(f => f.CodGrupoCausa == codigo);
        }

        public PagedList<GrupoCausa> ObterPorParametros(GrupoCausaParameters parameters)
        {
            var grupos = _context.GrupoCausa.AsQueryable();

            if (parameters.Filter != null)
            {
                grupos = grupos.Where(
                            g =>
                            g.CodGrupoCausa.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            g.CodEGrupoCausa.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            g.NomeGrupoCausa.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodGrupoCausa != null)
            {
                grupos = grupos.Where(g => g.CodGrupoCausa == parameters.CodGrupoCausa);
            }

            if (parameters.CodEGrupoCausa != null)
            {
                grupos = grupos.Where(g => g.CodEGrupoCausa == parameters.CodEGrupoCausa);
            }

            if (parameters.IndAtivo != null)
            {
                grupos = grupos.Where(g => g.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                grupos = grupos.OrderBy(parameters.SortActive, parameters.SortDirection);
            }

            return PagedList<GrupoCausa>.ToPagedList(grupos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
