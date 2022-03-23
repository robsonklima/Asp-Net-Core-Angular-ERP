using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public partial class AgendaTecnicoService : IAgendaTecnicoService
    {
        public void OrdenarAgendaTecnico(AgendaTecnicoParameters parameters)
        {
            List<AgendaTecnico> agendamentos =
                this.ObterAgenda(parameters.InicioPeriodoAgenda.Value, parameters.FimPeriodoAgenda.Value, parameters.CodTecnico.Value);

            switch (parameters.Ordenacao)
            {
                case AgendaTecnicoOrdenationEnum.FIM_SLA:
                    this.OrdernarPorFimSLA(agendamentos);
                    break;
                case AgendaTecnicoOrdenationEnum.MENOR_TRAGETORIA:
                    this.OrdernarPorMenorTragetoria(agendamentos);
                    break;
            }
        }

        private void OrdernarPorFimSLA(List<AgendaTecnico> agendamentos)
        {
            var agendamentosComSLA = agendamentos.Where(i => i.IndAgendamento == 0 &&
                i.Tipo == AgendaTecnicoTypeEnum.OS && i.OrdemServico.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO &&
                i.OrdemServico.PrazosAtendimento.Any())
            .OrderBy(i => i.OrdemServico.PrazosAtendimento
                .OrderByDescending(i => i.CodOSPrazoAtendimento)
                .FirstOrDefault().DataHoraLimiteAtendimento)
            .ToList();

            var agendamentosSemSLA = agendamentos.Where(i => i.IndAgendamento == 0 &&
                i.Tipo == AgendaTecnicoTypeEnum.OS && i.OrdemServico.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO &&
                !i.OrdemServico.PrazosAtendimento.Any())
            .OrderBy(i => i.Inicio).ToList();

            var listaAgendamentos = agendamentosComSLA.Concat(agendamentosSemSLA).ToList();

            this.ReordenaEventos(listaAgendamentos);
        }

        private void OrdernarPorMenorTragetoria(List<AgendaTecnico> agendasTecnico)
        {
            if (!agendasTecnico.Any()) return;

            var codTecnico = agendasTecnico.FirstOrDefault().CodTecnico.Value;
            var tecnico = this._tecnicoRepo.ObterPorCodigo(codTecnico);

            Localizacao ultimaLocalizacao = tecnico.Usuario.Localizacoes
                .OrderByDescending(i => i.CodLocalizacao)
                .FirstOrDefault();

            if (ultimaLocalizacao == null) {
                ultimaLocalizacao = new Localizacao
                {
                    Latitude = tecnico.Latitude,
                    Longitude = tecnico.Longitude
                };
            }

            var agendasTecnicosPertinentes = agendasTecnico
                .Where(
                    i => i.IndAgendamento == 0 &&
                    i.Tipo == AgendaTecnicoTypeEnum.OS && i.OrdemServico.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO
                )
                .ToList();

            List<AgendaTecnico> listaAgendamentos = new List<AgendaTecnico>();

            while (agendasTecnicosPertinentes.Any())
            {
                var distancias = new List<AgendaTecnicoDistanceModel>();

                agendasTecnicosPertinentes.ForEach(i =>
                {
                    var proximaLocalizacao = new Localizacao
                    {
                        Latitude = i.OrdemServico.LocalAtendimento.Latitude,
                        Longitude = i.OrdemServico.LocalAtendimento.Longitude
                    };

                    distancias.Add(new AgendaTecnicoDistanceModel
                    {
                        CodAgendaTecnico = i.CodAgendaTecnico,
                        Distancia = this.CalculaDistancia(ultimaLocalizacao, proximaLocalizacao),
                    });
                });

                var distsOrdened = distancias.OrderBy(i => i.Distancia).ToList();
                var minDistCodAgendaTecnico = distsOrdened.FirstOrDefault().CodAgendaTecnico;
                var minAgendaTecnico = agendasTecnicosPertinentes.FirstOrDefault(i => i.CodAgendaTecnico == minDistCodAgendaTecnico);

                ultimaLocalizacao = new Localizacao
                {
                    Latitude = minAgendaTecnico.OrdemServico.LocalAtendimento.Latitude,
                    Longitude = minAgendaTecnico.OrdemServico.LocalAtendimento.Longitude
                };

                listaAgendamentos.Add(minAgendaTecnico);
                agendasTecnicosPertinentes.Remove(minAgendaTecnico);
            }

            this.ReordenaEventos(listaAgendamentos);
        }

        private double CalculaDistancia(Localizacao inicial, Localizacao final)
        {
            double orig_lat, orig_long, dest_lat, dest_long;

            double.TryParse(inicial.Latitude, NumberStyles.Number, CultureInfo.InvariantCulture, out orig_lat);

            double.TryParse(inicial.Longitude, NumberStyles.Number, CultureInfo.InvariantCulture, out orig_long);

            double.TryParse(final.Latitude, NumberStyles.Number, CultureInfo.InvariantCulture, out dest_lat);

            double.TryParse(final.Longitude, NumberStyles.Number, CultureInfo.InvariantCulture, out dest_long);

            return this.GetDistanceInMinutesPerKm(orig_lat, orig_long, dest_lat, dest_long, 50);
        }

        private void ReordenaEventos(List<AgendaTecnico> agendasTecnico)
        {
            if (!agendasTecnico.Any()) return;

            var codTecnico = agendasTecnico.FirstOrDefault().CodTecnico;
            AgendaTecnico ultimoEvento = null;
            OrdemServico ultimaOS = null;

            agendasTecnico.ForEach(e =>
            {
                if (e.OrdemServico.CodStatusServico != (int)StatusServicoEnum.TRANSFERIDO)
                {
                    e.Cor = this.GetStatusColor((StatusServicoEnum)e.OrdemServico.CodStatusServico);
                    this._agendaRepo.Atualizar(e);
                }
                else
                {
                    OrdemServico os = e.OrdemServico;

                    var deslocamento = this.DistanciaEmMinutos(os, ultimaOS);
                    var start = ultimoEvento != null ? ultimoEvento.Fim : DateTime.Now;
                    var duracao = (e.Fim - e.Inicio).TotalMinutes;

                    // adiciona deslocamento
                    start = start.AddMinutes(deslocamento);

                    // se começa durante o intervalo ou depois do expediente
                    if (this.isIntervalo(start))
                        start = this.FimIntervalo(start);
                    else if (start >= this.FimExpediente(start))
                    {
                        start = start.AddDays(1);
                        start = this.InicioExpediente(start);
                    }

                    // se termina durante a sugestao de intervalo
                    var end = start.AddMinutes(duracao);
                    if (this.isIntervalo(end))
                    {
                        start = end.AddMinutes(deslocamento);
                        end = start.AddMinutes(duracao);
                    }

                    e.Inicio = start;
                    e.Fim = end;
                    e.CodUsuarioCad = Constants.SISTEMA_NOME;
                    e.DataHoraCad = DateTime.Now;
                    e.Cor = this.GetTypeColor(AgendaTecnicoTypeEnum.OS);
                    var ag = this._agendaRepo.Atualizar(e);

                    ultimoEvento = ag;
                    ultimaOS = os;
                }
            });
        }
    }
}