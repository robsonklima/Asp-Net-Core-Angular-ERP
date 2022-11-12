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
    public class ORTransporteRepository : IORTransporteRepository
    {
        private readonly AppDbContext _context;

        public ORTransporteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ORTransporte ORTransporte)
        {
            _context.ChangeTracker.Clear();
            ORTransporte p = _context.ORTransporte.FirstOrDefault(p => p.CodTransportadora == ORTransporte.CodTransportadora);

            if(p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(ORTransporte);
                _context.SaveChanges();
            }
        }

        public void Criar(ORTransporte ORTransporte)
        {
            _context.Add(ORTransporte);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            ORTransporte ORTransporte = _context.ORTransporte.FirstOrDefault(p => p.CodTransportadora == codigo);

            if (ORTransporte != null)
            {
                _context.ORTransporte.Remove(ORTransporte);
                _context.SaveChanges();
            }
        }

        public ORTransporte ObterPorCodigo(int codigo)
        {
            return _context.ORTransporte
                .FirstOrDefault(p => p.CodTransportadora == codigo);
        }

        public PagedList<ORTransporte> ObterPorParametros(ORTransporteParameters parameters)
        {
            var query = _context.ORTransporte
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    p =>
                    p.CodTransportadora.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    p.NomeTransportadora.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodTransportadora.HasValue)
            {
                query = query.Where(q => q.CodTransportadora == parameters.CodTransportadora);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ORTransporte>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
