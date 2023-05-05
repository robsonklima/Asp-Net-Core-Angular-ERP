using Microsoft.EntityFrameworkCore;
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
    public class CausaImprodutividadeRepository : ICausaImprodutividadeRepository
    {
        private readonly AppDbContext _context;

        public CausaImprodutividadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(CausaImprodutividade causaImprodutividade)
        {
            _context.ChangeTracker.Clear();
            CausaImprodutividade p = _context.CausaImprodutividade
                .FirstOrDefault(p => p.CodCausaImprodutividade == causaImprodutividade.CodCausaImprodutividade);
            try
            {
                if (p != null)
                {
                    _context.Entry(p).CurrentValues.SetValues(causaImprodutividade);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Criar(CausaImprodutividade causaImprodutividade)
        {
            try
            {
                _context.Add(causaImprodutividade);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codCausaImprodutividade)
        {
            CausaImprodutividade p = _context.CausaImprodutividade
                .FirstOrDefault(p => p.CodCausaImprodutividade == codCausaImprodutividade);

            if (p != null)
            {
                _context.CausaImprodutividade.Remove(p);
                _context.SaveChanges();
            }
        }

        public CausaImprodutividade ObterPorCodigo(int codCausaImprodutividade)
        {
            try
            {
                return _context.CausaImprodutividade
                    .Include(c => c.Improdutividade)
                    .SingleOrDefault(p => p.CodCausaImprodutividade == codCausaImprodutividade);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a CausaImprodutividade {ex.Message}");
            }
        }

        public PagedList<CausaImprodutividade> ObterPorParametros(CausaImprodutividadeParameters parameters)
        {
            var causaImprodutividades = _context.CausaImprodutividade
                    .Include(c => c.Improdutividade)
                .AsQueryable();

            if (parameters.CodProtocolo.HasValue)
            {
                causaImprodutividades = causaImprodutividades.Where(p => p.CodProtocolo == parameters.CodProtocolo);
            }

            if (parameters.CodImprodutividade.HasValue)
            {
                causaImprodutividades = causaImprodutividades.Where(p => p.CodImprodutividade == parameters.CodImprodutividade);
            }

            if (parameters.IndAtivo.HasValue)
            {
                causaImprodutividades = causaImprodutividades.Where(p => p.IndAtivo == parameters.IndAtivo);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                causaImprodutividades = causaImprodutividades.Where(a =>
                    a.CodCausaImprodutividade.ToString().Contains(parameters.Filter));
            }

            
            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                causaImprodutividades = causaImprodutividades.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<CausaImprodutividade>.ToPagedList(causaImprodutividades, parameters.PageNumber, parameters.PageSize);
        }

    }
}
