using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class GeolocalizacaoRepository : IGeolocalizacaoRepository
    {
        private readonly AppDbContext _context;

        public GeolocalizacaoRepository(AppDbContext context)
        {
            this._context = context;
        }

        public void Criar(Geolocalizacao geolocalizacao)
        {
            this._context.Add(geolocalizacao);
            this._context.SaveChanges();
        }

        public PagedList<Geolocalizacao> ObterPorParametros(GoogleGeolocationParameters parameters)
        {
            IQueryable<Geolocalizacao> localizacoes = this._context.Geolocalizacao.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.EnderecoCEP))
            {
                localizacoes = localizacoes.Where(e => e.EnderecoCEP.Equals(parameters.EnderecoCEP)); ;
            }

            return PagedList<Geolocalizacao>.ToPagedList(localizacoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
