using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class AuditoriaRepository : IAuditoriaRepository
    {
        private readonly AppDbContext _context;

        public AuditoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Auditoria auditoria)
        {
            _context.ChangeTracker.Clear();
            Auditoria aud = _context.Auditoria
                .FirstOrDefault(aud => aud.CodAuditoria == auditoria.CodAuditoria);
            try
            {
                if (aud != null)
                {
                    _context.Entry(aud).CurrentValues.SetValues(auditoria);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    

        public void Criar(Auditoria auditoria)
        {
            try
            {
                _context.Add(auditoria);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codAuditoria)
        {
            Auditoria aud = _context.Auditoria
                .FirstOrDefault(aud => aud.CodAuditoria == codAuditoria);

            if (aud != null)
            {
                _context.Auditoria.Remove(aud);
                _context.SaveChanges();
            }
        }

        public Auditoria ObterPorCodigo(int codAuditoria)
        {
            try
            {
                return _context.Auditoria.SingleOrDefault(aud => aud.CodAuditoria == codAuditoria);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public PagedList<Auditoria> ObterPorParametros(AuditoriaParameters parameters)
        {
            var auditorias = _context.Auditoria
                .AsQueryable();

            return PagedList<Auditoria>.ToPagedList(auditorias, parameters.PageNumber, parameters.PageSize);
        }
    }

}
