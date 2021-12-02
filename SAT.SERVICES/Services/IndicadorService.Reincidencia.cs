using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        private List<Indicador> ObterIndicadorReincidencia(IndicadorParameters parameters)
        {
            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.CLIENTE:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.REINCIDENCIA_CLIENTE.Description(), parameters.DataInicio, parameters.DataFim);
                case IndicadorAgrupadorEnum.FILIAL:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.REINCIDENCIA_FILIAL.Description(), parameters.DataInicio, parameters.DataFim);
                case IndicadorAgrupadorEnum.TECNICO_PERCENT_REINCIDENTES:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.REINCIDENCIA_TECNICO_PERCENT.Description(), parameters.DataInicio, parameters.DataFim);
                case IndicadorAgrupadorEnum.TECNICO_QNT_CHAMADOS_REINCIDENTES:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.REINCIDENCIA_TECNICO_QNT_CHAMADOS.Description(), parameters.DataInicio, parameters.DataFim);
                case IndicadorAgrupadorEnum.EQUIPAMENTO_PERCENT_REINCIDENTES:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.REINCIDENCIA_EQUIPAMENTO_PERCENT.Description(), parameters.DataInicio, parameters.DataFim);
                default:
                    return new List<Indicador>();
            }
        }

        private static List<Indicador> ObterIndicadorReincidenciaFilial(List<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new();

            var filiais = chamados
                .Where(os => os.Filial != null)
                .GroupBy(os => new { os.CodFilial, os.Filial.NomeFilial })
                .Select(os => new { filial = os.Key, Count = os.Count() });

            foreach (var f in filiais)
            {
                var osEquipamento = chamados
                    .Where(os => os.CodFilial == f.filial.CodFilial)
                    .GroupBy(os => new { os.CodEquipContrato })
                    .Select(os => new { equip = os.Key, Count = os.Count() });

                int reinc = 0;
                foreach (var os in osEquipamento)
                {
                    if (os.Count > 0)
                    {
                        reinc += os.Count - 1;
                    }
                }

                decimal valor = 100;
                try
                {
                    valor = decimal.Round((Convert.ToDecimal(reinc) / osEquipamento.Count()) * 100, 2, MidpointRounding.AwayFromZero);
                }
                catch (DivideByZeroException) { }

                Indicadores.Add(new Indicador()
                {
                    Label = f.filial.NomeFilial,
                    Valor = valor,
                    Filho = new List<Indicador>() { new Indicador() { Label = "Reincidencia" } }
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorReincidenciaCliente(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new();

            var clientes = chamados
                .Where(os => os.Cliente != null)
                .GroupBy(os => new { os.CodCliente, os.Cliente.NomeFantasia })
                .Select(os => new { cliente = os.Key, Count = os.Count() });

            foreach (var c in clientes)
            {
                var osEquipamento = chamados
                    .Where(os => os.CodCliente == c.cliente.CodCliente)
                    .GroupBy(os => new { os.CodEquipContrato })
                    .Select(os => new { equip = os.Key, Count = os.Count() });

                int reinc = 0;
                foreach (var os in osEquipamento)
                {
                    if (os.Count > 0)
                    {
                        reinc += os.Count - 1;
                    }
                }

                decimal valor = 100;
                try
                {
                    valor = decimal.Round((Convert.ToDecimal(reinc) / osEquipamento.Count()) * 100, 2, MidpointRounding.AwayFromZero);
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

        private List<Indicador> ObterIndicadorReincidenciaTecnicoPercent(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var tecnicos = chamados
                .Where(os => os.Tecnico != null)
                .GroupBy(os => os.Tecnico.CodTecnico)
                .Select(os => new { CodTecnico = os.Key, Count = os.Count() });

            foreach (var c in tecnicos)
            {
                var osEquipamento = chamados
                    .Where(os => os.Tecnico != null && os.Tecnico.CodTecnico == c.CodTecnico)
                    .GroupBy(os => new { os.CodEquipContrato })
                    .Select(os => new { equip = os.Key, Count = os.Count() });

                int reinc = 0;
                foreach (var os in osEquipamento)
                {
                    if (os.Count > 0)
                    {
                        reinc += os.Count - 1;
                    }
                }

                decimal valor = 100;
                try
                {
                    valor = decimal.Round((Convert.ToDecimal(reinc) / osEquipamento.Count()) * 100, 2, MidpointRounding.AwayFromZero);
                }
                catch (DivideByZeroException) { }

                Indicadores.Add(new Indicador()
                {
                    Label = c.CodTecnico.ToString(),
                    Valor = valor
                });
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorReincidenciaTecnicoQnt(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> listaIndicadores = new List<Indicador>();

            var tecnicos = chamados
                .Where(os => os.Tecnico != null)
                .GroupBy(os => os.Tecnico.CodTecnico)
                .Select(os => new { CodTecnico = os.Key, Count = os.Count() });

            foreach (var c in tecnicos)
            {
                listaIndicadores.AddRange(
                    chamados
                    .Where(os => os.CodTecnico == c.CodTecnico)
                    .GroupBy(os => new { os.CodEquipContrato })
                    .Select(os => new Indicador()
                    { Label = c.CodTecnico.ToString(), Valor = os.Count() })
                    .ToList());
            }

            return listaIndicadores;
        }

        private List<Indicador> ObterIndicadorEquipReincidentes(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new();

            var equipChamados = chamados
                                    .Where(os => (os.EquipamentoContrato != null))
                                    .GroupBy(os => os.EquipamentoContrato.CodEquipContrato)
                                    .Select(os => new { CodEquipContrato = os.Key, Count = os.Count(), Inicios = os.ToList().Select(c => c.DataHoraSolicitacao) });

            foreach (var item in equipChamados)
            {
                int reinc = 0;

                if (item.Count > 0)
                {
                    reinc += item.Count - 1;
                }

                var equip = equipChamados.FirstOrDefault(e => e.CodEquipContrato == item.CodEquipContrato);

                var calc = decimal.Round((Convert.ToDecimal(reinc) / equip.Count) * 100, 2, MidpointRounding.AwayFromZero);


                Indicadores.Add(new Indicador()
                {
                    Label = item.CodEquipContrato.ToString(),
                    Valor = calc
                });
            }

            return Indicadores;
        }
    }
}