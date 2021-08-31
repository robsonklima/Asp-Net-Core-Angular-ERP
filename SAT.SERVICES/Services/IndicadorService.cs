using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
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

        private IEnumerable<OrdemServico> ObterOrdensServico(IndicadorParameters parameters)
        {
            return _osRepository.ObterPorParametros(new OrdemServicoParameters()
            {
                DataAberturaInicio = parameters.DataInicio,
                DataAberturaFim = parameters.DataFim,
                CodClientes = parameters.CodClientes,
                CodFiliais = parameters.CodFiliais,
                CodStatusServicos = parameters.CodStatusServicos,
                CodTiposIntervencao = parameters.CodTiposIntervencao,
                PageSize = Int32.MaxValue
            }).Where(w => w.PrazosAtendimento.Count > 0);
        }

        public List<Indicador> ObterIndicadores(IndicadorParameters parameters)
        {
            switch (parameters.Tipo)
            {
                case IndicadorTipoEnum.ORDEM_SERVICO:
                    return ObterIndicadorOrdemServico(parameters);
                case IndicadorTipoEnum.SLA:
                    return ObterIndicadorSLA(parameters);
                case IndicadorTipoEnum.PENDENCIA:
                    return ObterIndicadorPendencia(parameters);
                case IndicadorTipoEnum.REINCIDENCIA:
                    return ObterIndicadorReincidencia(parameters);
                default:
                    throw new NotImplementedException("Não Implementado");
            }
        }


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

        private List<Indicador> ObterIndicadorOrdemServico(IndicadorParameters parameters)
        {
            var chamados = ObterOrdensServico(parameters);

            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.CLIENTE:
                    return ObterIndicadorOrdemServicoCliente(chamados);
                case IndicadorAgrupadorEnum.FILIAL:
                    return ObterIndicadorOrdemServicoFilial(chamados);
                case IndicadorAgrupadorEnum.STATUS_SERVICO:
                    return ObterIndicadorOrdemServicoStatusServico(chamados);
                case IndicadorAgrupadorEnum.TIPO_INTERVENCAO:
                    return ObterIndicadorOrdemServicoTipoIntervencao(chamados);
                case IndicadorAgrupadorEnum.DATA:
                    return ObterIndicadorOrdemServicoData(chamados);
                default:
                    throw new NotImplementedException("Não Implementado");
            }
        }

        private List<Indicador> ObterIndicadorPendencia(IndicadorParameters parameters)
        {
            List<Indicador> Indicadores = new List<Indicador>();
            var chamados = ObterOrdensServico(parameters);

            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.CLIENTE:
                    Indicadores = ObterIndicadorPendenciaCliente(chamados);
                    break;

                case IndicadorAgrupadorEnum.FILIAL:
                    Indicadores = ObterIndicadorPendenciaFilial(chamados);
                    break;
                default:
                    break;
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorReincidencia(IndicadorParameters parameters)
        {
            List<Indicador> Indicadores = new List<Indicador>();
            var chamados = ObterOrdensServico(parameters);

            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.CLIENTE:
                    Indicadores = ObterIndicadorReincidenciaCliente(chamados);
                    break;

                case IndicadorAgrupadorEnum.FILIAL:
                    Indicadores = ObterIndicadorReincidenciaFilial(chamados);
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

        private List<Indicador> ObterIndicadorOrdemServicoFilial(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var filiais = chamados
                        .Where(os => os.Filial != null)
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

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorOrdemServicoCliente(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var clientes = chamados
                .Where(os => os.Cliente != null)
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

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorOrdemServicoData(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var datas = chamados
                        .Where(os => os.DataHoraAberturaOS != null)
                        .GroupBy(os => new { os.DataHoraAberturaOS.Value.Date })
                        .Select(os => new { datas = os.Key, Count = os.Count() });

            foreach (var data in datas)
            {
                Indicadores.Add(new Indicador()
                {
                    Label = data.datas.Date.ToString(),
                    Valor = data.Count
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorOrdemServicoStatusServico(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var status = chamados
                         .Where(os => os.Filial != null)
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

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorOrdemServicoTipoIntervencao(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

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

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorPendenciaFilial(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var filiais = chamados
                .Where(os => os.Filial != null)
                .GroupBy(os => new { os.CodFilial, os.Filial.NomeFilial })
                .Select(os => new { filial = os.Key, Count = os.Count() });

            foreach (var f in filiais)
            {
                var pendentes = chamados
                    .Where(os => os.CodFilial == f.filial.CodFilial)
                    .Where(os => os.RelatoriosAtendimento
                        .Any(r => r.CodStatusServico == Constants.PECAS_PENDENTES))
                    .Count();

                var total = chamados
                    .Where(os => os.CodFilial == f.filial.CodFilial)
                    .Count();

                Indicadores.Add(new Indicador()
                {
                    Label = f.filial.NomeFilial,
                    Valor = decimal.Round((Convert.ToDecimal(pendentes) / total) * 100, 2, MidpointRounding.AwayFromZero)
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorPendenciaCliente(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var clientes = chamados
                .Where(os => os.Cliente != null)
                .GroupBy(os => new { os.CodCliente, os.Cliente.NomeFantasia })
                .Select(os => new { cliente = os.Key, Count = os.Count() });

            foreach (var c in clientes)
            {
                var pendentes = chamados
                    .Where(os => os.CodCliente == c.cliente.CodCliente)
                    .Where(os => os.RelatoriosAtendimento
                        .Any(r => r.CodStatusServico == Constants.PECAS_PENDENTES))
                    .Count();

                var total = chamados
                    .Where(os => os.CodCliente == c.cliente.CodCliente)
                    .Count();

                Indicadores.Add(new Indicador()
                {
                    Label = c.cliente.NomeFantasia,
                    Valor = decimal.Round((Convert.ToDecimal(pendentes) / total) * 100, 2, MidpointRounding.AwayFromZero)
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorReincidenciaFilial(IEnumerable<OrdemServico> chamados)
        {
            throw new NotImplementedException();
        }

        private List<Indicador> ObterIndicadorReincidenciaCliente(IEnumerable<OrdemServico> chamados)
        {
            throw new NotImplementedException();
        }
    }
}
