using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Extensions;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        private List<Indicador> ObterIndicadorPecaFaltante(IndicadorParameters parameters)
        {
            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.FILIAL:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.PECAS_FILIAL.Description(), parameters.DataInicio, parameters.DataFim);
                case IndicadorAgrupadorEnum.TOP_CINCO_PECAS_MAIS_FALTANTES:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.PECAS_TOP_CINCO_MAIS_FALTANTES.Description(), parameters.DataInicio, parameters.DataFim);
                case IndicadorAgrupadorEnum.TOP_PECAS_FALTANTES:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.PECAS_CRITICAS_MAIS_FALTANTES.Description(), parameters.DataInicio, parameters.DataFim);
                case IndicadorAgrupadorEnum.NOVAS_CADASTRADAS:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.PECAS_NOVAS_CADASTRADAS.Description(), parameters.DataInicio, parameters.DataFim);
                case IndicadorAgrupadorEnum.NOVAS_LIBERADAS:
                    return _dashboardService.ObterDadosIndicador(NomeIndicadorEnum.PECAS_NOVAS_LIBERADAS.Description(), parameters.DataInicio, parameters.DataFim);
                default:
                    return new List<Indicador>();
            }
        }

        private List<Indicador> ObterIndicadorPecasFaltantesFiliais(List<OrdemServico> chamados)
        {
            List<Indicador> indicadores = new();
            DateTime hoje = DateTime.Now;

            return (from os in chamados
                    from rat in os.RelatoriosAtendimento.Where(r => r.DataHoraCad < hoje)
                    from ratD in rat.RelatorioAtendimentoDetalhes.SelectMany(s => s.RelatorioAtendimentoDetalhePecas)
                    where !string.IsNullOrWhiteSpace(ratD.DescStatus) && ratD.DescStatus.Contains("PE�A FALTANTE")

                    &&
                    ratD.RelatorioAtendimentoDetalhePecaStatus
                    .Where(w => w.CodRatPecasStatus == (int)RelatorioAtendimentoPecaStatusEnum.ENCAMINHADA ||
                                w.CodRatPecasStatus == (int)RelatorioAtendimentoPecaStatusEnum.ENTREGUE)
                    .OrderByDescending(o => o.CodRATDetalhesPecasStatus).FirstOrDefault() == null

                    group new { os.Filial, DataFaltante = rat.DataHoraCad.Value.Date }
                    by new { os, DataFaltante = rat.DataHoraCad.Value.Date } into grupo
                    orderby grupo.Key.DataFaltante

                    select new Indicador()
                    {
                        Label = grupo.Key.DataFaltante.ToString("dd/MM/yyyy"),
                        Filho = new List<Indicador>() { new Indicador()
                                                  {
                                                   Label = grupo.Key.os.Filial.NomeFilial,
                                                   Valor = grupo.Count(),
                                                  }
                                     }
                    }).ToList();
        }

        private List<Indicador> ObterIndicadorCincoPecasMaisFaltantes(List<OrdemServico> chamados)
        {
            List<Indicador> indicadores = new();
            DateTime dataMinima = DateTime.Now.AddMonths(-3);

            return (from os in chamados
                    from rat in os.RelatoriosAtendimento.Where(r => r.DataHoraCad > dataMinima)
                    from ratD in rat.RelatorioAtendimentoDetalhes.SelectMany(s => s.RelatorioAtendimentoDetalhePecas)
                    where !string.IsNullOrWhiteSpace(ratD.DescStatus) && ratD.DescStatus.Contains("PE�A FALTANTE")
                    &&
                    ratD.RelatorioAtendimentoDetalhePecaStatus
                    .Where(w => w.CodRatPecasStatus == (int)RelatorioAtendimentoPecaStatusEnum.ENCAMINHADA ||
                                w.CodRatPecasStatus == (int)RelatorioAtendimentoPecaStatusEnum.ENTREGUE)
                    .OrderByDescending(o => o.CodRATDetalhesPecasStatus).FirstOrDefault() == null

                    group new { ratD.Peca.CodMagnus, ratD.Peca.NomePeca }
                    by new { ratD.Peca.CodMagnus, ratD.Peca.NomePeca } into grupo
                    orderby grupo.Count() descending

                    select new Indicador()
                    {
                        Label = grupo.Key.CodMagnus,
                        Filho = new List<Indicador>() { new Indicador()
                                                  {
                                                   Label = grupo.Key.NomePeca,
                                                   Valor = grupo.Count(),
                                                  }
                                     }
                    }).Take(5).ToList();
        }

        private List<Indicador> ObterIndicadorPecasCriticasFaltantes(List<OrdemServico> chamados, IndicadorAgrupadorEnum indicador)
        {
            List<Indicador> indicadores = new();

            IEnumerable<RelatorioAtendimento> pecasPendentes = chamados
                                 .Where(os => os.RelatoriosAtendimento != null && os.CodStatusServico != (int)StatusServicoEnum.CANCELADO && os.CodStatusServico != (int)StatusServicoEnum.FECHADO)
                                 .SelectMany(rt => rt.RelatoriosAtendimento)
                                 .Where(rt => rt.CodStatusServico == (int)StatusServicoEnum.PECAS_PENDENTES);

            IEnumerable<RelatorioAtendimentoDetalhePeca> detalhePeca = pecasPendentes
                .Where(pc => pc.RelatorioAtendimentoDetalhes != null)
                                .SelectMany(pc => pc.RelatorioAtendimentoDetalhes
                                                      .SelectMany(rdp => rdp.RelatorioAtendimentoDetalhePecas.Where(p => p.Peca != null)));

            detalhePeca.GroupBy(dp => dp.Peca).OrderByDescending(p => p.Count()).Take(10).ToList()
            .ForEach(pc =>
            {
                indicadores.Add(new Indicador()
                {
                    Label = pc.Key.CodPeca.ToString(),
                    Valor = pc.Count(),
                    Filho = ObterChamadosPecaFaltante(pc.Key.CodPeca, pecasPendentes, indicador)
                });
            });

            return indicadores;
        }

        private List<Indicador> ObterChamadosPecaFaltante(int codPeca, IEnumerable<RelatorioAtendimento> pecasPendentes, IndicadorAgrupadorEnum indicador)
        {
            List<Indicador> indicadores = new();

            List<RelatorioAtendimento> rats = pecasPendentes.Where(r => r.RelatorioAtendimentoDetalhes
            .Select(rd => rd.RelatorioAtendimentoDetalhePecas
            .Select(rdp => rdp.CodPeca))
            .Any(p => p.Any(pp => pp == codPeca)))
            .Distinct().ToList();

            rats.ForEach(r =>
            {
                indicadores.Add(new Indicador()
                {
                    Label = indicador == IndicadorAgrupadorEnum.NOVAS_CADASTRADAS ?
                                                    r.DataCadastro.ToString() :
                                                 r.DataHoraSolucao.ToString(),
                    Valor = r.CodOS
                });
            });

            return indicadores;
        }
    }
}