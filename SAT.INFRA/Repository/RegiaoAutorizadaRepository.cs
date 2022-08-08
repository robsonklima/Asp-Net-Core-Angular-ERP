using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class RegiaoAutorizadaRepository : IRegiaoAutorizadaRepository
    {
        private readonly AppDbContext _context;

        public RegiaoAutorizadaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Criar(RegiaoAutorizada regiaoAutorizada)
        {
            try
            {
                _context.Add(regiaoAutorizada);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codRegiao, int codAutorizada, int codFilial)
        {
            RegiaoAutorizada ra = _context.RegiaoAutorizada.SingleOrDefault(
                             ra => ra.CodAutorizada == codAutorizada &&
                                   ra.CodRegiao == codRegiao &&
                                   ra.CodFilial == codFilial);

            if (ra != null)
            {
                _context.RegiaoAutorizada.Remove(ra);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }

        public RegiaoAutorizada ObterPorCodigo(int codRegiao, int codAutorizada, int codFilial)
        {
            return _context.RegiaoAutorizada
                .Include(ra => ra.Regiao)
                .Include(ra => ra.Autorizada)
                .Include(ra => ra.Filial)
                .Include(ra => ra.Cidade)
                .SingleOrDefault(
                    ra =>
                    ra.CodAutorizada == codAutorizada &&
                    ra.CodRegiao == codRegiao &&
                    ra.CodFilial == codFilial);
        }

        public IQueryable<RegiaoAutorizada> ObterQuery(RegiaoAutorizadaParameters parameters)
        {
            IQueryable<RegiaoAutorizada> query = _context.RegiaoAutorizada.AsQueryable();

            query = AplicarIncludes(query);
            query = AplicarFiltros(query, parameters);
            query = AplicarOrdenacao(query, parameters.SortActive, parameters.SortDirection);

            return query.AsNoTracking();
        }

        public PagedList<RegiaoAutorizada> ObterPorParametros(RegiaoAutorizadaParameters parameters)
        {
           IQueryable<RegiaoAutorizada> query = this.ObterQuery(parameters);

            return PagedList<RegiaoAutorizada>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
