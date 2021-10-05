using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class GeolocalizacaoService : IGeolocalizacaoService
    {
        private readonly IGeolocalizacaoRepository _iGeolocalizacaoRepository;

        public GeolocalizacaoService(IGeolocalizacaoRepository iGeolocalizacaoRepository)
        {
            this._iGeolocalizacaoRepository = iGeolocalizacaoRepository;
        }

        public Geolocalizacao Criar(Geolocalizacao geolocalizacao)
        {
            this._iGeolocalizacaoRepository.Criar(geolocalizacao);
            return geolocalizacao;
        }

        public ListViewModel ObterPorParametros(GoogleGeolocationParameters parameters)
        {
            PagedList<Geolocalizacao> localizacoes = _iGeolocalizacaoRepository.ObterPorParametros(parameters);
            return new ListViewModel() { Items = localizacoes };
        }
    }
}
