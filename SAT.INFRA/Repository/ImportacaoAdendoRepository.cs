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
    public class ImportacaoAdendoRepository : IImportacaoAdendoRepository
    {
        private readonly AppDbContext _context;

        public ImportacaoAdendoRepository(AppDbContext context)
        {
            _context = context;
        }

        public ImportacaoAdendo Atualizar(ImportacaoAdendo adendo)
        {
            _context.ChangeTracker.Clear();
            ImportacaoAdendo a = _context.ImportacaoAdendo.SingleOrDefault(a => a.CodAdendo == adendo.CodAdendo);

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

        public ImportacaoAdendo Criar(ImportacaoAdendo adendo)
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

        public ImportacaoAdendo Deletar(int codigo)
        {
            ImportacaoAdendo a = _context.ImportacaoAdendo.SingleOrDefault(a => a.CodAdendo == codigo);

            if (a != null)
            {
                try
                {
                    _context.ImportacaoAdendo.Remove(a);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }

            return a;
        }

        public ImportacaoAdendo ObterPorCodigo(int codigo)
        {
            return _context.ImportacaoAdendo.SingleOrDefault(a => a.CodAdendo == codigo);
        }

        public PagedList<ImportacaoAdendo> ObterPorParametros(ImportacaoAdendoParameters parameters)
        {
            var query = _context.ImportacaoAdendo.AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    c =>
                    c.CodAdendo.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodAdendo.HasValue)
            {
                query = query.Where(i => i.CodAdendo == parameters.CodAdendo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ImportacaoAdendo>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
