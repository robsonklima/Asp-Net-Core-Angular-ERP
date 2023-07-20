using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class AdendoRepository : IAdendoRepository
    {
        private readonly AppDbContext _context;

        public AdendoRepository(AppDbContext context)
        {
            _context = context;
        }

        public Adendo Atualizar(Adendo adendo)
        {
            _context.ChangeTracker.Clear();
            Adendo a = _context.Adendo.SingleOrDefault(a => a.CodAdendo == adendo.CodAdendo);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(adendo);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }

            return adendo;
        }

        public Adendo Criar(Adendo adendo)
        {
            try
            {
                _context.Add(adendo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }

            return adendo;
        }

        public Adendo Deletar(int codigo)
        {
            Adendo a = _context.Adendo.SingleOrDefault(a => a.CodAdendo == codigo);

            if (a != null)
            {
                try
                {
                    _context.Adendo.Remove(a);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }

            return a;
        }

        public Adendo ObterPorCodigo(int codigo)
        {
            return _context.Adendo.SingleOrDefault(a => a.CodAdendo == codigo);
        }

        public PagedList<Adendo> ObterPorParametros(AdendoParameters parameters)
        {
            var query = _context.Adendo.AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    c =>
                    c.CodAdendo.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodEquipContrato.HasValue)
            {
                query = query.Where(a => a.Itens.Any(i => i.CodEquipContrato == parameters.CodEquipContrato));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Adendo>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
