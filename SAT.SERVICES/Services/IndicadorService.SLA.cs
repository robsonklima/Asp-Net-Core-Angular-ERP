using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Extensions;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        private List<Indicador> ObterIndicadorSLA(IndicadorParameters parameters)
        {
            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.CLIENTE:
                    return this.AgrupadorIndicadorCliente(parameters.DataInicio, parameters.DataFim);
                case IndicadorAgrupadorEnum.FILIAL:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.SLA_FILIAL.Description(), parameters.DataInicio, parameters.DataFim);
                default:
                    return new List<Indicador>();
            }
        }
        private List<Indicador> AgrupadorIndicadorCliente(DateTime dataInicio, DateTime dataFim)
        {
            return (from ind in _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.SLA_CLIENTE.Description(), dataInicio, dataFim)
                    group ind by ind.Label into grupo
                    select new Indicador
                    {
                        Label = grupo.Key,
                        Valor = grupo.Sum(s => s.Valor) / grupo.Count(),
                        Filho = grupo.FirstOrDefault().Filho
                    }).ToList();
        }

        private List<Indicador> ObterIndicadorSLACliente(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> indicadores = new();

            foreach (var cliente in chamados.Select(s => new { s.Cliente.CodCliente, s.Cliente.NomeFantasia }).Distinct())
            {
                var chamadosCliente = chamados.Where(r => r.CodCliente == cliente.CodCliente && r.CodStatusServico != (int)StatusServicoEnum.CANCELADO)
                         .GroupBy(g => g.CodCliente).FirstOrDefault();

                // N�o encontrou dados
                if (chamadosCliente == null) continue;

                var dados = (from ch in chamadosCliente
                             let chamadoNaoFechado = ch.RelatoriosAtendimento != null && ch.RelatoriosAtendimento.Count <= 0
                             let dataLimite = !chamadoNaoFechado ?
                                              ch.RelatoriosAtendimento.OrderByDescending(m => m.DataHoraSolucao).FirstOrDefault().DataHoraSolucao
                                              : ch.DataHoraFechamento
                             select new
                             {
                                 dentro =
                                   chamadoNaoFechado || (
                                 ch.PrazosAtendimento.OrderByDescending(m => m.DataHoraLimiteAtendimento).FirstOrDefault().DataHoraLimiteAtendimento
                                 >= dataLimite)
                             }).ToList();

                decimal countDentro = dados.Count(d => d.dentro);
                decimal percent = dados.Count > 0 ? ((countDentro / dados.Count) * 100) : 100;

                indicadores.Add(new Indicador()
                {
                    Label = cliente.NomeFantasia,
                    Valor = decimal.Round(percent, 2, MidpointRounding.AwayFromZero)
                });
            }

            return indicadores;
        }

        private static List<Indicador> ObterIndicadorSLAFilial(List<OrdemServico> chamados)
        {
            List<Indicador> indicadores = new();

            foreach (var filial in chamados.Select(s => new { s.Filial.CodFilial, s.Filial.NomeFilial }).Distinct())
            {
                var chamadosFilial = chamados.Where(r => r.CodFilial == filial.CodFilial && r.CodStatusServico != (int)StatusServicoEnum.CANCELADO
                              && r.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA)
                             .GroupBy(g => g.CodCliente).FirstOrDefault();

                // N�o encontrou dados
                if (chamadosFilial == null) continue;

                var dados = (from ch in chamadosFilial
                             let chamadoNaoFechado = ch.RelatoriosAtendimento != null && ch.RelatoriosAtendimento.Count <= 0
                             let dataLimite = !chamadoNaoFechado ?
                                              ch.RelatoriosAtendimento.OrderByDescending(m => m.DataHoraSolucao).FirstOrDefault().DataHoraSolucao
                                              : ch.DataHoraFechamento
                             select new
                             {
                                 dentro =
                                   chamadoNaoFechado || (
                                 ch.PrazosAtendimento.OrderByDescending(m => m.DataHoraLimiteAtendimento).FirstOrDefault().DataHoraLimiteAtendimento
                                 >= dataLimite)
                             }).ToList();

                decimal countDentro = dados.Count(d => d.dentro);
                decimal percent = dados.Count > 0 ? ((countDentro / dados.Count) * 100) : 100;

                indicadores.Add(new Indicador()
                {
                    Label = filial.NomeFilial,
                    Valor = decimal.Round(percent, 2, MidpointRounding.AwayFromZero),
                    Filho = new List<Indicador>() { new Indicador() { Label = "SLA" } }
                }); ;
            }

            return indicadores;
        }
    }
}