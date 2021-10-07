using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IGeolocalizacaoService
    {
        ListViewModel ObterPorParametros(GoogleGeolocationParameters parameters);
        Geolocalizacao Criar(Geolocalizacao geolocalizacao);
    }
}
