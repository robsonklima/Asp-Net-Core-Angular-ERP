using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
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
                    Indicadores = ObterIndicadorTopPecaFaltante(chamados);
                    break;
                default:
                    break;
            }

            return Indicadores;
        }

        private List<Indicador> ObterIndicadorTopPecaFaltante(IEnumerable<OrdemServico> chamados)
        {
            List<Indicador> Indicadores = new List<Indicador>();

            var pecasPendentes = chamados
                                .Where(os => os.RelatoriosAtendimento != null)
                                .SelectMany(rt => rt.RelatoriosAtendimento)
                                .Where(rt => rt.CodStatusServico == (int)StatusServicoEnum.PECAS_PENDENTES)
                                .ToList();

            var detalhePeca = pecasPendentes.Where(pc => pc.RelatorioAtendimentoDetalhes != null)
                                .SelectMany(pc => pc.RelatorioAtendimentoDetalhes
                                                      .SelectMany(rdp => rdp.RelatorioAtendimentoDetalhePecas))
                                .ToList();

            detalhePeca.GroupBy(dp => dp.Peca).ToList().ForEach(pc =>
            {
                Indicadores.Add(new Indicador()
                {
                    Label = pc.Key.CodPeca.ToString(),
                    Valor = pc.Count()
                });
            });

            return Indicadores;
        }
    }
}