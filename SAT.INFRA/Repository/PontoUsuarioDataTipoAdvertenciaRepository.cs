using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Repository
{
    public class PontoUsuarioDataTipoAdvertenciaRepository : IPontoUsuarioDataTipoAdvertenciaRepository
    {
        private readonly AppDbContext _context;

        public PontoUsuarioDataTipoAdvertenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<PontoUsuarioDataTipoAdvertencia> ObterPorParametros(PontoUsuarioDataTipoAdvertenciaParameters parameters)
        {
            var query = _context.PontoUsuarioDataTipoAdvertencia.AsQueryable();

            return PagedList<PontoUsuarioDataTipoAdvertencia>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
