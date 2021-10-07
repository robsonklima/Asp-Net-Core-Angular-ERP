using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Helpers;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IGeolocalizacaoRepository
    {
        PagedList<Geolocalizacao> ObterPorParametros(GoogleGeolocationParameters parameters);
        void Criar(Geolocalizacao geolocalizacao);
    }
}
