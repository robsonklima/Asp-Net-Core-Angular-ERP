using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Threading.Tasks;

namespace SAT.SERVICES.Interfaces
{
    public interface IGeolocalizacaoService
    {
        ListViewModel ObterPorParametros(GoogleGeolocationParameters parameters);
        Geolocalizacao Criar(Geolocalizacao geolocalizacao);
        Task<GoogleGeolocation> ObterGeolocalizacao(GoogleGeolocationParameters parameters);
        Task<DistanceMatrixResponse> GetDistance(GoogleGeolocationParameters parameters);
    }
}
