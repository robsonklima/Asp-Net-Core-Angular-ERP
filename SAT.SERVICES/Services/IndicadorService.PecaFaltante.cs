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
        private List<Indicador> ObterIndicadorPecaFaltante(IndicadorParameters parameters)
        {
            List<Indicador> Indicadores = new List<Indicador>();
            var chamados = ObterOrdensServico(parameters);

            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.TOP_PECAS_FALTANTES:
                    Indicadores = ObterIndicadorTopPecaFaltante(chamados, IndicadorAgrupadorEnum.TOP_PECAS_FALTANTES);
                    break;
                case IndicadorAgrupadorEnum.NOVAS_CADASTRADAS:
                    Indicadores = ObterIndicadorTopPecaFaltante(chamados,IndicadorAgrupadorEnum.NOVAS_CADASTRADAS);
                    break;
                case IndicadorAgrupadorEnum.NOVAS_LIBERADAS:
                    Indicadores = ObterIndicadorTopPecaFaltante(chamados,IndicadorAgrupadorEnum.NOVAS_LIBERADAS);
                    break;
                default:
                    break;
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorTopPecaFaltante(IEnumerable<OrdemServico> chamados,IndicadorAgrupadorEnum indicador)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var pecasPendentes = chamados
                                .Where(os => os.RelatoriosAtendimento != null && os.CodStatusServico != 2 && os.CodStatusServico != 3)
                                .SelectMany(rt => rt.RelatoriosAtendimento)
                                .Where(rt => rt.CodStatusServico == (int)StatusServicoEnum.PECAS_PENDENTES)
                                .ToList();
            
            var detalhePeca = pecasPendentes.Where(pc => pc.RelatorioAtendimentoDetalhes != null)
                                .SelectMany(pc => pc.RelatorioAtendimentoDetalhes
                                                      .SelectMany(rdp => rdp.RelatorioAtendimentoDetalhePecas))
                                .ToList();

            detalhePeca
                    .GroupBy(dp => dp.Peca)
                    .OrderByDescending(p => p.Count())
                    .Take(10)
                    .ToList()
                    .ForEach(pc =>
                    {
                        Indicadores.Add(new Indicador()
                        {
                            Label = pc.Key.CodPeca.ToString(),
                            Valor = pc.Count(),
                            Filho = ObterChamadosPecaFaltante(pc.Key.CodPeca, pecasPendentes,indicador)
                        });
                    });

            return Indicadores;
        }
        private List<Indicador> ObterChamadosPecaFaltante(int codPeca, List<RelatorioAtendimento> pecasPendentes, IndicadorAgrupadorEnum indicador)
        {
            var Indicadores = new List<Indicador>();
            
            var rats = pecasPendentes.Where(r => r.RelatorioAtendimentoDetalhes
            .Select(rd => rd.RelatorioAtendimentoDetalhePecas
            .Select(rdp => rdp.CodPeca))
            .Any(p => p.Any(pp => pp == codPeca)))
            .Distinct().ToList();

            rats.ForEach(r => 
            {
                Indicadores.Add(new Indicador()
                {   
                    Label = indicador == IndicadorAgrupadorEnum.NOVAS_CADASTRADAS ?
                                                    r.DataCadastro.ToString() : 
                                                 r.DataHoraSolucao.ToString() ,
                    Valor = r.CodOS
                });
            });

            return Indicadores;
        }
    }
}