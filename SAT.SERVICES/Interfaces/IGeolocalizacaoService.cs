using SAT.MODELS.Entities;
using System.Threading.Tasks;

namespace SAT.SERVICES.Interfaces
{
    public interface IGeolocalizacaoService
    {
        Task<Geolocalizacao> ObterGeolocalizacao(GeolocalizacaoParameters parameters);
        Task<Geolocalizacao> BuscarRota(GeolocalizacaoParameters parameters);
    }
}
