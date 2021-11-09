using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace SAT.INFRA.Repository
{
    public class PecaRepository : IPecaRepository
    {
        private readonly AppDbContext _context;

        public PecaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Peca peca)
        {
            Peca p = _context.Peca.FirstOrDefault(p => p.CodPeca == peca.CodPeca);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(peca);
                _context.SaveChanges();
            }
        }

        public void Criar(Peca peca)
        {
            try
            {
                _context.Add(peca);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Deletar(int codPeca)
        {
            Peca p = _context.Peca.FirstOrDefault(p => p.CodPeca == codPeca);

            if (p != null)
            {
                _context.Peca.Remove(p);
                _context.SaveChanges();
            }
        }

        public Peca ObterPorCodigo(int codigo)
        {
            return _context.Peca.FirstOrDefault(p => p.CodPeca == codigo);
        }

        public PagedList<Peca> ObterPorParametros(PecaParameters parameters)
        {
            var pecas = _context.Peca
                .Include(p => p.PecaStatus)
                .Include(p => p.PecaFamilia)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
            {
                pecas = pecas.Where(p =>
                    p.CodPeca.ToString().Contains(parameters.Filter) ||
                    p.CodMagnus.Contains(parameters.Filter) ||
                    p.NomePeca.Contains(parameters.Filter)
                );
            }
      
            if (!string.IsNullOrEmpty(parameters.CodPeca))
            {
                var split = parameters.CodPeca.Split(",").Select(int.Parse).ToArray();
                pecas = pecas.Where(p => split.Any(s => s.Equals(p.CodPeca)));
            }

            if (!string.IsNullOrEmpty(parameters.CodMagnus))
            {
                pecas = pecas.Where(p => p.CodPeca == Convert.ToInt32(parameters.CodPeca));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                pecas = pecas.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Peca>.ToPagedList(pecas, parameters.PageNumber, parameters.PageSize);
        }
    }
}