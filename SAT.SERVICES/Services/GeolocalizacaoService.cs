using Newtonsoft.Json;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SAT.SERVICES.Services
{
    public class GeolocalizacaoService : IGeolocalizacaoService
    {
        private readonly IGeolocalizacaoRepository _iGeolocalizacaoRepository;
        private readonly ICidadeService _cidadeService;

        public GeolocalizacaoService(IGeolocalizacaoRepository iGeolocalizacaoRepository, ICidadeService cidadeService)
        {
            this._iGeolocalizacaoRepository = iGeolocalizacaoRepository;
            this._cidadeService = cidadeService;
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

        public async Task<GoogleGeolocation> ObterGeolocalizacao(GoogleGeolocationParameters parameters)
        {
            // Verifica se existe no banco a o endereço antes
            ListViewModel localizacoes = this.ObterPorParametros(parameters);
            GoogleGeolocation model = new();

            if (((List<Geolocalizacao>)localizacoes.Items).Count > 0)
            {
                string json = ((List<Geolocalizacao>)localizacoes.Items)[0].GoogleGeolocationJson;
                model = JsonConvert.DeserializeObject<GoogleGeolocation>(json);
            }
            else
            {
                HttpClient client = new();

                var response = await client.GetAsync
                    ($"https://maps.googleapis.com/maps/api/geocode/json?address=CEP-{parameters.EnderecoCEP}-Brazil&key=AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM");

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var conteudo = await response.Content.ReadAsStringAsync();
                    model = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleGeolocation>(conteudo);

                    if (model.results.Count > 0)
                    {
                        Geolocalizacao novaLocalizacao = new()
                        {
                            EnderecoCEP = parameters.EnderecoCEP,
                            Latitude = model.results[0].geometry.location.lat.ToString(),
                            Longitude = model.results[0].geometry.location.lng.ToString(),
                            GoogleGeolocationJson = conteudo
                        };

                        this.Criar(novaLocalizacao);
                    }
                }
            }

            // Busca os Cods cadastrados no sistema
            if (model.results.Count > 0)
            {
                AddressComponent posicaoCidade = model.results[0].address_components[3];
                if (posicaoCidade != null)
                {
                    string formataCidade = Regex.Replace(posicaoCidade.long_name, "[^0-9a-zA-Z]+", "").ToLower();

                    Cidade procuraCidade = _cidadeService.BuscaCidadePorNome(formataCidade);
                    if (procuraCidade != null)
                    {
                        model.results[0].dadosSAT = new()
                        {
                            CodCidade = procuraCidade.CodCidade,
                            CodUF = procuraCidade.UnidadeFederativa.CodUF,
                            CodPais = procuraCidade.UnidadeFederativa.Pais.CodPais
                        };
                    }
                }
            }

            return model;
        }

        public async Task<DistanceMatrixResponse> GetDistance(GoogleGeolocationParameters parameters)
        {
            DistanceMatrixResponse model = new();

            var response = await new HttpClient().GetAsync("https://maps.googleapis.com/maps/api/distancematrix/json?destinations=" + parameters.LatitudeDestino + "%2C" + parameters.LongitudeDestino + "&origins=" + parameters.LatitudeOrigem + "%2C" + parameters.LongitudeOrigem + "&key=AIzaSyC4StJs8DtJZZIELzFgJckwrsvluzRo_WM");

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var conteudo = await response.Content.ReadAsStringAsync();
                model = Newtonsoft.Json.JsonConvert.DeserializeObject<DistanceMatrixResponse>(conteudo);
            }

            return model;
        }
    }
}
