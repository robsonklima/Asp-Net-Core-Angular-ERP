using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace SAT.INFRA.Repository
{
    public partial class PecaRE5114Repository : IPecaRE5114Repository
    {
        private readonly AppDbContext _context;

        public PecaRE5114Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PecaRE5114 peca)
        {
            _context.ChangeTracker.Clear();
            PecaRE5114 p = _context.PecaRE5114.FirstOrDefault(p => p.CodPecaRe5114 == peca.CodPecaRe5114);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(peca);
                _context.SaveChanges();
            }
        }

        public void Criar(PecaRE5114 peca)
        {
            try
            {
                _context.Add(peca);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codPecaRE5114)
        {
            PecaRE5114 p = _context.PecaRE5114.FirstOrDefault(p => p.CodPecaRe5114 == codPecaRE5114);

            if (p != null)
            {
                _context.PecaRE5114.Remove(p);
                _context.SaveChanges();
            }
        }

        public PecaRE5114 ObterPorCodigo(int codigo)
        {
            return _context.PecaRE5114
            .Include(p => p.Peca)
            .FirstOrDefault(p => p.CodPecaRe5114 == codigo);
        }

        public PagedList<PecaRE5114> ObterPorParametros(PecaRE5114Parameters parameters)
        {
             IQueryable<PecaRE5114> pecaRE5114 = _context.PecaRE5114
                .Include(i => i.Peca)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                pecaRE5114 = pecaRE5114.Where(
                    s =>
                    s.CodPecaRe5114.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.Peca.CodPeca.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
             );

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                pecaRE5114 = pecaRE5114.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodPecas))
            {
                int[] cods = parameters.CodPecas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                pecaRE5114 = pecaRE5114.Where(dc => cods.Contains(dc.Peca.CodPeca));
            }

            if (!string.IsNullOrWhiteSpace(parameters.NumRe5114))
            {
                string[] cods = parameters.NumRe5114.Split(",").Select(a => a.Trim()).Distinct().ToArray();
                pecaRE5114 = pecaRE5114.Where(dc => cods.Contains(dc.NumRe5114));
            }

            return PagedList<PecaRE5114>.ToPagedList(pecaRE5114, parameters.PageNumber, parameters.PageSize);
        }
    }
}