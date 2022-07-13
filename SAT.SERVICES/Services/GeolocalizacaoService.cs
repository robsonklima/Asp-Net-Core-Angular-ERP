using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System.Threading.Tasks;

namespace SAT.SERVICES.Services
{
    public partial class GeolocalizacaoService : IGeolocalizacaoService
    {
        public GeolocalizacaoService() { }

        public async Task<Geolocalizacao> ObterGeolocalizacao(GeolocalizacaoParameters parameters)
        {
            switch (parameters.GeolocalizacaoServiceEnum)
            {
                case GeolocalizacaoServiceEnum.GOOGLE:
                    return await GoogleLocalizationService(parameters);
                case GeolocalizacaoServiceEnum.NOMINATIM:
                    return await NominatimLocalizationService(parameters);
                case GeolocalizacaoServiceEnum.VIACEP:
                    return await ViacepLocalizationService(parameters);
                default:
                    return null;
            }
        }

        public async Task<Geolocalizacao> BuscarRota(GeolocalizacaoParameters parameters)
        {
            switch (parameters.GeolocalizacaoServiceEnum)
            {
                case GeolocalizacaoServiceEnum.GOOGLE:
                    return await GoogleRouteService(parameters);
                case GeolocalizacaoServiceEnum.NOMINATIM:
                    return await NominatimRouteService(parameters);
                case GeolocalizacaoServiceEnum.VIACEP:
                    return await ViacepLocalizationService(parameters);
                default:
                    return null;
            }
        }
    }
}