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
    public partial class PecaStatusRepository : IPecaStatusRepository
    {
        private readonly AppDbContext _context;

        public PecaStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(PecaStatus pecaStatus)
        {
            _context.ChangeTracker.Clear();
            PecaStatus p = _context.PecaStatus.FirstOrDefault(p => p.CodPecaStatus == pecaStatus.CodPecaStatus);

            if (p != null)
            {
                _context.Entry(p).CurrentValues.SetValues(pecaStatus);
                _context.SaveChanges();
            }
        }

        public void Criar(PecaStatus pecaStatus)
        {
            try
            {
                _context.Add(pecaStatus);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codPecaStatus)
        {
            PecaStatus p = _context.PecaStatus.FirstOrDefault(p => p.CodPecaStatus == codPecaStatus);

            if (p != null)
            {
                _context.PecaStatus.Remove(p);
                _context.SaveChanges();
            }
        }

        public PecaStatus ObterPorCodigo(int codigo)
        {
            return _context.PecaStatus.FirstOrDefault(p => p.CodPecaStatus == codigo);
        }

        public IQueryable<PecaStatus> ObterQuery(PecaStatusParameters parameters)
        {
            var query = _context
                            .PecaStatus
                            .AsQueryable();

            return query;
        }

        public PagedList<PecaStatus> ObterPorParametros(PecaStatusParameters parameters)
        {
            var query = this.ObterQuery(parameters);

            return PagedList<PecaStatus>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}