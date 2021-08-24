using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.API.Repositories
{
    public class ContratoSLARepository : IContratoSLARepository
    {
        private readonly AppDbContext _context;

        public ContratoSLARepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<ContratoSLA> ObterPorParametros(ContratoSLAParameters parameters)
        {
            var contratosSLA = _context.ContratoSLA
                .Include(c => c.Contrato)
                .Include(c => c.SLA)
                .AsQueryable();

            if (parameters.CodContrato != null)
            {
                contratosSLA = contratosSLA.Where(a => a.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodSLA != null)
            {
                contratosSLA = contratosSLA.Where(a => a.CodSLA == parameters.CodSLA);
            }

            return PagedList<ContratoSLA>.ToPagedList(contratosSLA, parameters.PageNumber, parameters.PageSize);
        }
    }
}
