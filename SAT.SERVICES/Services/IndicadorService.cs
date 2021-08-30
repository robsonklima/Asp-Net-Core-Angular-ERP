using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.SERVICES.Services
{
    public class IndicadorService : IIndicadorService
    {
        private readonly IOrdemServicoRepository _osRepository;

        public IndicadorService(IOrdemServicoRepository osRepository)
        {
            _osRepository = osRepository;
        }

        public List<Indicador> ObterIndicadores(IndicadorParameters parameters)
        {
            switch (parameters.Tipo)
            {
                case IndicadorTipoEnum.ORDEM_SERVICO:
                    return ObterIndicadorOrdemServico(parameters);
                case IndicadorTipoEnum.SLA:
                    return ObterIndicadorSLA(parameters);
                default:
                    return null;
            }
        }

        private List<Indicador> ObterIndicadorSLA(IndicadorParameters parameters)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var chamados = _osRepository.ObterPorParametros(new OrdemServicoParameters()
            {
                DataAberturaInicio = parameters.DataInicio,
                DataAberturaFim = parameters.DataFim,
                CodClientes = parameters.CodClientes,
                CodFiliais = parameters.CodFiliais,
                CodStatusServicos = parameters.CodStatusServicos,
                CodTiposIntervencao = parameters.CodTiposIntervencao,
                PageSize = Int32.MaxValue
            }).Where(w => w.PrazosAtendimento.Count > 0);


            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.CLIENTE:
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
                        var p = (Convert.ToDecimal(d) / (f + d)) * 100;

                        Indicadores.Add(new Indicador()
                        {
                            Label = c.cliente.NomeFantasia,
                            Valor = decimal.Round(p, 2, MidpointRounding.AwayFromZero)
                        });
                    }

                    break;

                case IndicadorAgrupadorEnum.FILIAL:
                    var filiais = chamados
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
                        var p = (Convert.ToDecimal(d) / (f + d)) * 100;

                        Indicadores.Add(new Indicador()
                        {
                            Label = filial.filial.NomeFilial,
                            Valor = decimal.Round(p, 2, MidpointRounding.AwayFromZero)
                        });
                    }

                    break;
                default:
                    break;
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorOrdemServico(IndicadorParameters parameters)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var chamados = _osRepository.ObterPorParametros(new OrdemServicoParameters()
            {
                DataAberturaInicio = parameters.DataInicio,
                DataAberturaFim = parameters.DataFim,
                CodClientes = parameters.CodClientes,
                CodFiliais = parameters.CodFiliais,
                CodStatusServicos = parameters.CodStatusServicos,
                CodTiposIntervencao = parameters.CodTiposIntervencao,
                PageSize = Int32.MaxValue
            });

            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.CLIENTE:
                    var clientes = chamados
                        .GroupBy(os => new { os.CodCliente, os.Cliente.NomeFantasia })
                        .Select(os => new { cliente = os.Key, Count = os.Count() });

                    foreach (var cliente in clientes)
                    {
                        Indicadores.Add(new Indicador()
                        {
                            Label = cliente.cliente.NomeFantasia,
                            Valor = cliente.Count
                        });
                    }

                    break;

                case IndicadorAgrupadorEnum.FILIAL:
                    var filiais = chamados
                        .GroupBy(os => new { os.CodFilial, os.Filial.NomeFilial })
                        .Select(os => new { filial = os.Key, Count = os.Count() });

                    foreach (var filial in filiais)
                    {
                        Indicadores.Add(new Indicador()
                        {
                            Label = filial.filial.NomeFilial,
                            Valor = filial.Count
                        });
                    }

                    break;

                case IndicadorAgrupadorEnum.STATUS_SERVICO:
                    var status = chamados
                        .GroupBy(os => new { os.CodStatusServico, os.StatusServico.NomeStatusServico })
                        .Select(os => new { status = os.Key, Count = os.Count() });

                    foreach (var st in status)
                    {
                        Indicadores.Add(new Indicador()
                        {
                            Label = st.status.NomeStatusServico,
                            Valor = st.Count
                        });
                    }

                    break;

                case IndicadorAgrupadorEnum.TIPO_INTERVENCAO:
                    var tipos = chamados
                        .GroupBy(os => new { os.CodTipoIntervencao, os.TipoIntervencao.NomTipoIntervencao })
                        .Select(os => new { tipos = os.Key, Count = os.Count() });

                    foreach (var tipo in tipos)
                    {
                        Indicadores.Add(new Indicador()
                        {
                            Label = tipo.tipos.NomTipoIntervencao,
                            Valor = tipo.Count
                        });
                    }

                    break;
                default:
                    break;
            }

            return Indicadores;
        }
    }
}
