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
    public class ORDefeitoRepository : IORDefeitoRepository
    {
        private readonly AppDbContext _context;

        public ORDefeitoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ORDefeito orDefeito)
        {
            _context.ChangeTracker.Clear();
            ORDefeito p = _context.ORDefeito.FirstOrDefault(p => p.CodDefeito == orDefeito.CodDefeito);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(orDefeito);
                _context.SaveChanges();
            }
        }

        public void Criar(ORDefeito orDefeito)
        {
            _context.Add(orDefeito);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            ORDefeito ORDefeito = _context.ORDefeito.FirstOrDefault(p => p.CodDefeito == codigo);

            if (ORDefeito != null)
            {
                _context.ORDefeito.Remove(ORDefeito);
                _context.SaveChanges();
            }
        }

        public ORDefeito ObterPorCodigo(int codigo)
        {
            return _context.ORDefeito
                .FirstOrDefault(p => p.CodDefeito == codigo);
        }

        public PagedList<ORDefeito> ObterPorParametros(ORDefeitoParameters parameters)
        {
            var query = _context.ORDefeito
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodDefeito.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) 
                    ||
                    p.Descricao.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }
            
            if (parameters.CodDefeito.HasValue)
            {
                query = query.Where(q => q.CodDefeito == parameters.CodDefeito);
            }

            if(parameters.IndAtivo.HasValue)
            {
                query = query.Where(q => q.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ORDefeito>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
