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
            List<Indicador> Indicadores = new List<Indicador>();
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

        private List<Indicador> ObterIndicadorSLAFilial(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var filiais = chamados
                .Where(os => os.Filial != null)
                .GroupBy(os => new { os.Filial.CodFilial, os.Filial.NomeFilial })
                .Select(os => new { filial = os.Key, Count = os.Count() });

            foreach (var filial in filiais)
            {
                var dentro = chamados
                    .Where(
                        os => os.PrazosAtendimento
                            .OrderByDescending(o => o.DataHoraLimiteAtendimento)
                            .FirstOrDefault().DataHoraLimiteAtendimento >= os.DataHoraFechamento
                        && os.CodFilial == filial.filial.CodFilial
                    )
                    .GroupBy(os => new { os.CodCliente, os.Cliente.NomeFantasia })
                    .Select(os => new { d = os.Key, Count = os.Count() });

                var fora = chamados
                    .Where(
                        os => os.PrazosAtendimento
                            .OrderByDescending(o => o.DataHoraLimiteAtendimento)
                            .FirstOrDefault().DataHoraLimiteAtendimento < os.DataHoraFechamento
                        && os.CodCliente == filial.filial.CodFilial
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
                    Label = filial.filial.NomeFilial,
                    Valor = decimal.Round(p, 2, MidpointRounding.AwayFromZero)
                });
            }

            return Indicadores;
        }
    }
}