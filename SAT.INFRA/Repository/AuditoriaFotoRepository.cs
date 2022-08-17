using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class AuditoriaFotoRepository : IAuditoriaFotoRepository
    {
        private readonly AppDbContext _context;

        public AuditoriaFotoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(AuditoriaFoto auditoriaFoto)
        {
            _context.ChangeTracker.Clear();
            AuditoriaFoto aud = _context.AuditoriaFoto
                .FirstOrDefault(aud => aud.CodAuditoriaFoto == auditoriaFoto.CodAuditoriaFoto);
            try
            {
                if (aud != null)
                {
                    _context.Entry(aud).CurrentValues.SetValues(auditoriaFoto);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    

        public void Criar(AuditoriaFoto auditoriaFoto)
        {
            try
            {
                _context.Add(auditoriaFoto);
                _context.SaveChanges();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public void Deletar(int codAuditoriaFoto)
        {
            AuditoriaFoto aud = _context.AuditoriaFoto
                .FirstOrDefault(aud => aud.CodAuditoriaFoto == codAuditoriaFoto);

            if (aud != null)
            {
                _context.AuditoriaFoto.Remove(aud);
                _context.SaveChanges();
            }
        }

        public AuditoriaFoto ObterPorCodigo(int codAuditoriaFoto)
        {
            try
            {
                return _context.AuditoriaFoto.SingleOrDefault(aud => aud.CodAuditoriaFoto == codAuditoriaFoto);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public PagedList<AuditoriaFoto> ObterPorParametros(AuditoriaFotoParameters parameters)
        {
            var auditoriasFoto = _context.AuditoriaFoto
                .AsNoTracking()
                .AsQueryable();

            return PagedList<AuditoriaFoto>.ToPagedList(auditoriasFoto, parameters.PageNumber, parameters.PageSize);
        }
    }

}
