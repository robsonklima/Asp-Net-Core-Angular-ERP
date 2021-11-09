using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Repository
{
    public class PontoUsuarioDataMotivoDivergenciaRepository : IPontoUsuarioDataMotivoDivergenciaRepository
    {
        private readonly AppDbContext _context;

        public PontoUsuarioDataMotivoDivergenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<PontoUsuarioDataMotivoDivergencia> ObterPorParametros(PontoUsuarioDataMotivoDivergenciaParameters parameters)
        {
            var query = _context.PontoUsuarioDataMotivoDivergencia.AsQueryable();

            return PagedList<PontoUsuarioDataMotivoDivergencia>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
