using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        private List<Indicador> ObterIndicadorSLA(IndicadorParameters parameters)
        {
            List<Indicador> Indicadores = new();
            var chamados = ObterOrdensServico(parameters);

            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.CLIENTE:
                    Indicadores = ObterIndicadorSLACliente(chamados);
                    break;

                case IndicadorAgrupadorEnum.FILIAL:
                    Indicadores = ObterIndicadorSLAFilial(chamados);
                    break;
                default:
                    break;
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorSLACliente(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var clientes = chamados
                .GroupBy(os => new { os.CodCliente, os.Cliente.NomeFantasia })
                .Select(os => new { cliente = os.Key, Count = os.Count() });

            foreach (var c in clientes)
            {
                var dentro = chamados
                    .Where(
                        os => os.PrazosAtendimento
                            .OrderByDescending(o => o.DataHoraLimiteAtendimento)
                            .FirstOrDefault().DataHoraLimiteAtendimento >= os.DataHoraFechamento
                        && os.CodCliente == c.cliente.CodCliente
                    )
                    .GroupBy(os => new { os.CodCliente, os.Cliente.NomeFantasia })
                    .Select(os => new { d = os.Key, Count = os.Count() });

                var fora = chamados
                    .Where(
                        os => os.PrazosAtendimento
                            .OrderByDescending(o => o.DataHoraLimiteAtendimento)
                            .FirstOrDefault().DataHoraLimiteAtendimento < os.DataHoraFechamento
                        && os.CodCliente == c.cliente.CodCliente
                    )
                    .GroupBy(os => new { os.CodCliente, os.Cliente.NomeFantasia })
                    .Select(os => new { f = os.Key, Count = os.Count() });

                var d = dentro.Sum(d => d.Count);
                var f = fora.Sum(d => d.Count);
                var s = f + d;
                decimal p = 0;
                if (s > 0)
                {
                    p = (Convert.ToDecimal(d) / (s)) * 100;
                }
                else
                {
                    p = 100;
                }

                Indicadores.Add(new Indicador()
                {
                    Label = c.cliente.NomeFantasia,
                    Valor = decimal.Round(p, 2, MidpointRounding.AwayFromZero)
                });
            }

            return Indicadores;
        }

        private static List<Indicador> ObterIndicadorSLAFilial(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new();

            foreach (var filial in chamados.Select(s => new { s.Filial.CodFilial, s.Filial.NomeFilial }).Distinct())
            {
                var dados = (from ch in chamados.Where(r => r.CodFilial == filial.CodFilial && r.CodStatusServico != 2/*cancelado*/)
                             .GroupBy(g => g.CodCliente).FirstOrDefault()
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

                Indicadores.Add(new Indicador()
                {
                    Label = filial.NomeFilial,
                    Valor = decimal.Round(percent, 2, MidpointRounding.AwayFromZero)
                });
            }

            return Indicadores;
        }
    }
}