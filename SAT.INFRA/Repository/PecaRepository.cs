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
    public partial class PecaRepository : IPecaRepository
    {
        private readonly AppDbContext _context;

        public PecaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Peca peca)
        {
            _context.ChangeTracker.Clear();
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
            catch (Exception ex)
            {
                throw new Exception($"", ex);
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

        public IQueryable<Peca> ObterQuery(PecaParameters parameters)
        {
            var query = _context.Peca.AsNoTracking().AsQueryable();

            query = AplicarIncludes(query, parameters.Include);
            query = AplicarFiltros(query, parameters);
            query = AplicarOrdenacao(query, parameters.SortActive, parameters.SortDirection);

            return query;
        }

        public PagedList<Peca> ObterPorParametros(PecaParameters parameters)
        {
            var pecas = this.ObterQuery(parameters).AsQueryable();

            return PagedList<Peca>.ToPagedList(pecas, parameters.PageNumber, parameters.PageSize);
        }
    }
}