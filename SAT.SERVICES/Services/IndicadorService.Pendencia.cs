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
    public partial class IndicadorService : IIndicadorService
    {
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
                case IndicadorAgrupadorEnum.TECNICO_PERCENT_PENDENTES:
                    Indicadores = ObterIndicadorPendenciaTecnicoPercent(chamados);
                    break;
                case IndicadorAgrupadorEnum.TECNICO_QNT_CHAMADOS_PENDENTES:
                    Indicadores = ObterIndicadorPendenciaTecnicoQnt(chamados);
                    break;
                default:
                    break;
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

                decimal valor = 100;
                try
                {
                    valor = decimal.Round((Convert.ToDecimal(pendentes) / total) * 100, 2, MidpointRounding.AwayFromZero);
                }
                catch (DivideByZeroException) { }

                Indicadores.Add(new Indicador()
                {
                    Label = f.filial.NomeFilial,
                    Valor = valor
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

                decimal valor = 100;
                try
                {
                    valor = decimal.Round((Convert.ToDecimal(pendentes) / total) * 100, 2, MidpointRounding.AwayFromZero);
                }
                catch (DivideByZeroException) { }

                Indicadores.Add(new Indicador()
                {
                    Label = c.cliente.NomeFantasia,
                    Valor = valor
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorPendenciaTecnicoPercent(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var ratsTecnicos = chamados.Where(ch => ch.RelatoriosAtendimento != null)
                                    .SelectMany(ch => ch.RelatoriosAtendimento)
                                    .Where(ch => ch.Tecnico != null && Convert.ToBoolean(ch.Tecnico.IndAtivo))
                                    .GroupBy(ch => ch.CodTecnico).ToList();
           
            ratsTecnicos.ForEach(r => 
            {
                var ratsPecasPendentes = r.ToList().Where(rt => rt.CodStatusServico == (int)StatusServicoEnum.PECAS_PENDENTES).Count();
                var ratsTotais = r.ToList().Count();
                
                decimal valor = 0;

                if (ratsTotais > 0)
                {
                    valor = decimal.Round((Convert.ToDecimal(ratsPecasPendentes) / ratsTotais) * 100, 2, MidpointRounding.AwayFromZero);
                }

                Indicadores.Add(new Indicador()
                {
                    Label = r.Key.ToString(),
                    Valor = valor
                });
            });

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorPendenciaTecnicoQnt(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> batata =  chamados.Where(ch => ch.RelatoriosAtendimento != null)
                            .SelectMany(ch => ch.RelatoriosAtendimento)
                            //.Where(rat => rat.CodStatusServico == (int)StatusServicoEnum.PECAS_PENDENTES)
                            .Where(rat => rat.Tecnico != null && Convert.ToBoolean(rat.Tecnico.IndAtivo))
                            .GroupBy(rat => rat.CodTecnico)
                            .Select(ratsPorTecnico => new Indicador 
                            {
                                Label = ratsPorTecnico.Key.ToString(),
                                Valor = ratsPorTecnico.Count() 
                            }).ToList(); 

                            return batata;
        }
    }
}