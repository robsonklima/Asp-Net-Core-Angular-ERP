using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities.Constants;
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

        public void Atualizar(RegiaoAutorizada regiaoAutorizada, int codRegiao, int codAutorizada, int codFilial)
        {
            _context.ChangeTracker.Clear();
            regiaoAutorizada.Filial = null;
            regiaoAutorizada.Autorizada = null;
            regiaoAutorizada.Regiao = null;
            regiaoAutorizada.Cidade = null;
            RegiaoAutorizada ra = _context.RegiaoAutorizada.SingleOrDefault(ra =>
                                                                ra.CodAutorizada == codAutorizada &&
                                                                ra.CodRegiao == codRegiao &&
                                                                ra.CodFilial == codFilial);

            if (ra != null)
            {
                try
                {
                    _context.Entry(ra).CurrentValues.SetValues(regiaoAutorizada);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void Criar(RegiaoAutorizada regiaoAutorizada)
        {
            try
            {
                _context.Add(regiaoAutorizada);
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
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
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
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
