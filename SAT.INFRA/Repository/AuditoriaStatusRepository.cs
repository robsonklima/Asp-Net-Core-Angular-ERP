using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class AuditoriaStatusRepository : IAuditoriaStatusRepository
    {
        private readonly AppDbContext _context;

        public AuditoriaStatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(AuditoriaStatus auditoriaStatus)
        {
            _context.ChangeTracker.Clear();
            AuditoriaStatus aud = _context.AuditoriaStatus
                .FirstOrDefault(aud => aud.CodAuditoriaStatus == auditoriaStatus.CodAuditoriaStatus);
            try
            {
                if (aud != null)
                {
                    _context.Entry(aud).CurrentValues.SetValues(auditoriaStatus);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    

        public void Criar(AuditoriaStatus auditoriaStatus)
        {
            try
            {
                _context.Add(auditoriaStatus);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codAuditoriaStatus)
        {
            AuditoriaStatus aud = _context.AuditoriaStatus
                .FirstOrDefault(aud => aud.CodAuditoriaStatus == codAuditoriaStatus);

            if (aud != null)
            {
                _context.AuditoriaStatus.Remove(aud);
                _context.SaveChanges();
            }
        }

        public AuditoriaStatus ObterPorCodigo(int codAuditoriaStatus)
        {
            try
            {
                return _context.AuditoriaStatus.SingleOrDefault(aud => aud.CodAuditoriaStatus == codAuditoriaStatus);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public PagedList<AuditoriaStatus> ObterPorParametros(AuditoriaStatusParameters parameters)
        {
            var auditoriasStatus = _context.AuditoriaStatus
                .AsNoTracking()
                .AsQueryable();

            return PagedList<AuditoriaStatus>.ToPagedList(auditoriasStatus, parameters.PageNumber, parameters.PageSize);
        }
    }

}
