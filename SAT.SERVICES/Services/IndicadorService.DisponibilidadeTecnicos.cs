using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterIndicadorDisponibilidadeTecnicos(IndicadorParameters parameters)
        {
            return _dashboardService.ObterIndicadorDisponibilidadeTecnicos(
                NomeIndicadorEnum.DISPONIBILIDADE_TECNICOS.Description(),
                parameters.DataInicio, parameters.DataFim);
        }

        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterDadosDashboardTecnicoDisponibilidade(List<Tecnico> query, TecnicoParameters parameters)
        {
            DateTime agora = DateTime.Now;

            List<DashboardTecnicoDisponibilidadeTecnicoViewModel> retorno = new();

            foreach (var tecnico in query.Where(q => q.IndAtivo == 1 && q.Usuario != null &&
                                                     q.Usuario.IndAtivo == 1 && q.Filial != null).ToArray())
            {
                PontoUsuario[] pontoUsuario = tecnico.Usuario.PontosUsuario.Where(p =>
                        p.DataHoraRegistro >= parameters.PeriodoMediaAtendInicio &&
                        p.DataHoraRegistro <= parameters.PeriodoMediaAtendFim &&
                        tecnico.Usuario.CodUsuario == p.CodUsuario && p.IndAtivo == 1).ToArray();

                // Se por algum motivo não tem ponto ou chamados, não tem porque contabilizar 
                if (pontoUsuario.Length == 0 || tecnico.OrdensServico.Count == 0) continue;

                double diasTrabalhados = pontoUsuario.Max(s => s.DataHoraRegistro).Subtract(pontoUsuario.Min(s => s.DataHoraRegistro)).TotalDays;

                IEnumerable<OrdemServico> osTecnico = tecnico.OrdensServico.Where(os =>
                                       os.DataHoraAberturaOS >= parameters.PeriodoMediaAtendInicio &&
                                       os.DataHoraAberturaOS <= parameters.PeriodoMediaAtendFim &&
                                       os.RelatoriosAtendimento != null && os.RelatoriosAtendimento.Count > 0);

                retorno.Add(new DashboardTecnicoDisponibilidadeTecnicoViewModel()
                {
                   // Usuario = tecnico.Usuario,
                    IndFerias = tecnico.IndFerias,
                    IndAtivo = tecnico.IndAtivo,
                    CodTecnico = tecnico.CodTecnico,
                    CodFilial = tecnico.Filial.CodFilial,
                    NomeFilial = tecnico.Filial.NomeFilial,

                    TecnicoSemChamadosTransferidos = !tecnico.OrdensServico.Any(w => w.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO),

                    MediaAtendimentosPorDiaTodasIntervencoes = osTecnico.Where(os =>
                                          os.CodTipoIntervencao != (int)TipoIntervencaoEnum.AUTORIZACAO_DESLOCAMENTO &&
                                          os.CodTipoIntervencao != (int)TipoIntervencaoEnum.HELPDESK &&
                                          os.CodTipoIntervencao != (int)TipoIntervencaoEnum.HELP_DESK_DSS
                                           ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                         rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                         rat.DataHoraSolucao >= parameters.PeriodoMediaAtendInicio) / diasTrabalhados,

                    MediaAtendimentosPorDiaCorretivos = osTecnico.Where(os =>
                                          os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA
                                                    ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                                  rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                                  rat.DataHoraSolucao >= parameters.PeriodoMediaAtendInicio) / diasTrabalhados,

                    MediaAtendimentosPorDiaPreventivos = osTecnico.Where(os =>
                                         os.CodTipoIntervencao == (int)TipoIntervencaoEnum.PREVENTIVA
                                            ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                            rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                            rat.DataHoraSolucao >= parameters.PeriodoMediaAtendInicio) / diasTrabalhados,

                    MediaAtendimentosPorDiaInstalacoes = osTecnico.Where(os =>
                                        os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSTALACAO
                                            ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                            rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                            rat.DataHoraSolucao >= parameters.PeriodoMediaAtendInicio) / diasTrabalhados,

                    MediaAtendimentosPorDiaEngenharia = osTecnico.Where(os =>
                                        os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ALTERACAO_DE_ENGENHARIA
                                            ).SelectMany(r => r.RelatoriosAtendimento).Count(rat =>
                                            rat.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                            rat.DataHoraSolucao >= parameters.PeriodoMediaAtendInicio) / diasTrabalhados,
                });
            }

            return retorno;
        }
    }
}