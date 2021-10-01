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
    public partial class IndicadorService : IIndicadorService
    {
        private List<Indicador> ObterIndicadorSPA(IndicadorParameters parameters)
        {
            List<Indicador> Indicadores = new List<Indicador>();
            var chamados = ObterOrdensServico(parameters);

            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.CLIENTE:
                    Indicadores = ObterIndicadorSPACliente(chamados);
                    break;

                case IndicadorAgrupadorEnum.FILIAL:
                    Indicadores = ObterIndicadorSPAFilial(chamados);
                    break;
                default:
                    break;
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorSPACliente(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var clientes = chamados
                .GroupBy(os => new { os.CodCliente, os.Cliente.NomeFantasia })
                .Select(os => new { cliente = os.Key, Count = os.Count() });

            foreach (var cliente in clientes)
            {
                var chamadosCliente = chamados
                    .Where(os => os.RelatoriosAtendimento != null)
                    .Where(os => os.CodCliente == cliente.cliente.CodCliente);

                int spaForaQtd = 0;
                foreach (var os in chamadosCliente)
                {
                    if (os.RelatoriosAtendimento.Count() > 1)
                    {
                        spaForaQtd++;
                    }

                    var chamadosSPA = chamadosCliente
                        .Where(
                            o =>
                            o.CodEquipContrato == os.CodEquipContrato &&
                            o.DataHoraAberturaOS >= os.DataHoraAberturaOS &&
                            o.DataHoraAberturaOS <= os.DataHoraAberturaOS.Value.AddDays(3) &&
                            o.CodTipoIntervencao == Constants.CORRETIVA &&
                            o.CodOS != os.CodOS
                        );

                    if (chamadosSPA.Count() > 0)
                    {
                        spaForaQtd++;
                    }
                }

                decimal valor = 100;
                try
                {
                    valor = valor - decimal.Round((Convert.ToDecimal(spaForaQtd) / chamadosCliente.Count()) * 100, 2, MidpointRounding.AwayFromZero);
                }
                catch (DivideByZeroException) { }

                Indicadores.Add(new Indicador()
                {
                    Label = cliente.cliente.NomeFantasia,
                    Valor = valor
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorSPAFilial(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var filiais = chamados
                .Where(os => os.Filial != null)
                .GroupBy(os => new { os.Filial.CodFilial, os.Filial.NomeFilial })
                .Select(os => new { filial = os.Key, Count = os.Count() });

            foreach (var filial in filiais)
            {
                var chamadosFilial = chamados
                    .Where(os => os.RelatoriosAtendimento != null)
                    .Where(os => os.CodFilial == filial.filial.CodFilial);

                int spaForaQtd = 0;
                foreach (var os in chamadosFilial)
                {
                    if (os.RelatoriosAtendimento.Count() > 1)
                    {
                        spaForaQtd++;
                    }

                    var chamadosSPA = chamadosFilial
                        .Where(
                            o =>
                            o.CodEquipContrato == os.CodEquipContrato &&
                            o.DataHoraAberturaOS >= os.DataHoraAberturaOS &&
                            o.DataHoraAberturaOS <= os.DataHoraAberturaOS.Value.AddDays(3) &&
                            o.CodTipoIntervencao == Constants.CORRETIVA &&
                            o.CodOS != os.CodOS
                        );

                    if (chamadosSPA.Count() > 0)
                    {
                        spaForaQtd++;
                    }
                }

                decimal valor = 100;
                try
                {
                    valor = valor - decimal.Round((Convert.ToDecimal(spaForaQtd) / chamadosFilial.Count()) * 100, 2, MidpointRounding.AwayFromZero);
                }
                catch (DivideByZeroException) { }

                Indicadores.Add(new Indicador()
                {
                    Label = filial.filial.NomeFilial,
                    Valor = valor
                });
            }

            return Indicadores;
        }
    }
}
