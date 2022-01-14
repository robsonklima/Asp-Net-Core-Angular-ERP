using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class DeslocamentoService : IDeslocamentoService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly IGeolocalizacaoService _geolocalizacaoService;

        public DeslocamentoService(
            IOrdemServicoRepository ordemServicoRepo,
            IGeolocalizacaoService geolocalizacaoService)
        {
            _ordemServicoRepo = ordemServicoRepo;
            _geolocalizacaoService = geolocalizacaoService;
        }

        public async Task<ListViewModel> ObterPorParametrosAsync(DeslocamentoParameters parameters)
        {
            var deslocamentos = new List<Deslocamento>();
            var deslocamentoParameters = new OrdemServicoParameters()
            {
                CodTecnico = parameters.CodTecnico,
                Include = OrdemServicoIncludeEnum.OS_INTENCAO,
                DataHoraInicioInicio = parameters.DataHoraInicioInicio,
                DataHoraInicioFim = parameters.DataHoraInicioFim
            };

            deslocamentos.AddRange(
                _ordemServicoRepo.ObterQuery(deslocamentoParameters).Take(parameters.PageSize).ToList().Select(os =>
                {
                    double latDestino, lngDestino;
                    double.TryParse(os?.LocalAtendimento?.Latitude, NumberStyles.Number, CultureInfo.InvariantCulture, out latDestino);
                    double.TryParse(os?.LocalAtendimento?.Longitude, NumberStyles.Number, CultureInfo.InvariantCulture, out lngDestino);
                    var intencao = os?.Intencoes?.FirstOrDefault(i => i.CodTecnico == parameters.CodTecnico);
                    var checkin = os?.RelatoriosAtendimento?.FirstOrDefault(i => i.CodTecnico == parameters.CodTecnico)?.CheckinsCheckouts?.FirstOrDefault(i => i.Tipo == "CHECKIN");

                    return new Deslocamento
                    {
                        Origem = new DeslocamentoOrigem
                        {
                            Descricao = "Intenção",
                            Lat = intencao?.Latitude,
                            Lng = intencao?.Longitude,
                        },
                        Destino = new DeslocamentoDestino
                        {
                            Descricao = $"{os?.LocalAtendimento?.NomeLocal} - {os?.LocalAtendimento?.Endereco}, {os?.LocalAtendimento?.Cidade?.NomeCidade}.",
                            Lat = latDestino != 0 ? latDestino : null,
                            Lng = lngDestino != 0 ? lngDestino : null,
                        },
                        TempoCheckin = intencao != null && checkin != null ? (checkin.DataHoraCadSmartphone - intencao.DataHoraCad).Value.TotalMinutes : 0,
                        Data = checkin?.DataHoraCadSmartphone ?? intencao.DataHoraCad,
                        Tipo = DeslocamentoTipoEnum.INTENCAO,
                    };
                }));

            foreach (var d in deslocamentos.Where(d => d.Origem.Lat.HasValue && d.Origem.Lng.HasValue && d.Destino.Lat.HasValue && d.Destino.Lng.HasValue))
            {
                var l = await this.ObterDeslocamentoAsync(d);
                d.Distancia = l.Distancia;
                d.Tempo = l.Duracao;
                d.Origem.Descricao = $"{l.EnderecoOrigem}, {l.CidadeOrigem}, {l.EstadoOrigem}.";
            }

            var listaPaginada = PagedList<Deslocamento>.ToPagedList(deslocamentos.OrderBy(i => i.Data).ToList(), parameters.PageNumber, parameters.PageSize);
            var lista = new ListViewModel
            {
                Items = listaPaginada,
                TotalCount = listaPaginada.TotalCount,
                CurrentPage = listaPaginada.CurrentPage,
                PageSize = listaPaginada.PageSize,
                TotalPages = listaPaginada.TotalPages,
                HasNext = listaPaginada.HasNext,
                HasPrevious = listaPaginada.HasPrevious
            };

            return lista;
        }

        private async Task<Geolocalizacao> ObterDeslocamentoAsync(Deslocamento d) =>
         await this._geolocalizacaoService.BuscarRota(new GeolocalizacaoParameters
         {
             LatitudeOrigem = d.Origem.Lat.ToString(),
             LongitudeOrigem = d.Origem.Lng.ToString(),
             LatitudeDestino = d.Destino.Lat.ToString(),
             LongitudeDestino = d.Destino.Lng.ToString(),
             GeolocalizacaoServiceEnum = GeolocalizacaoServiceEnum.NOMINATIM
         });
    }
}
