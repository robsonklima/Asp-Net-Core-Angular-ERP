using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        /// <summary>
        /// Atualiza os do indicador no banco de dados
        /// </summary>
        /// <param name="nomeIndicador">Identificador do indicador</param>
        /// <param name="parameters"></param>
        private void AtualizaIndicador(string nomeIndicador, IndicadorParameters parameters)
        {
            DateTime dataInicio = parameters.DataInicio;
            DateTime dataFim = parameters.DataFim;

            List<OrdemServico> chamados = ObterOrdensServico(parameters).ToList();

          //  int diferencaDias = (int)dataFim.Subtract(dataInicio).TotalDays;

           // for (int i = 0; i <= diferencaDias; i++)
          //  {
              //  DateTime data = dataInicio.AddDays(i);
               // List<OrdemServico> chamadosDoDia = chamados.Where(s => s.DataHoraAberturaOS.Value.Date == data.Date).ToList();

               // if (!chamadosDoDia.Any()) continue;

                List<Indicador> dadosDB = new();

                //if (nomeIndicador == NomeIndicadorEnum.SLA_FILIAL.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorSLAFilial(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.SLA_CLIENTE.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorSLACliente(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.SPA_CLIENTE.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorSPACliente(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.SPA_FILIAL.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorSPAFilial(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.SPA_TECNICO_PERCENT.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorSPATecnicoPercent(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.SPA_TECNICO_QNT_CHAMADOS.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorSPATecnicoQnt(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.PENDENCIA_CLIENTE.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorPendenciaCliente(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.PENDENCIA_FILIAL.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorPendenciaFilial(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.PENDENCIA_TECNICO_PERCENT.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorPendenciaTecnicoPercent(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.PENDENCIA_TECNICO_QNT_CHAMADOS.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorPendenciaTecnicoQnt(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.REINCIDENCIA_CLIENTE.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorReincidenciaCliente(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.REINCIDENCIA_FILIAL.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorReincidenciaFilial(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.REINCIDENCIA_TECNICO_PERCENT.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorReincidenciaTecnicoPercent(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.REINCIDENCIA_TECNICO_QNT_CHAMADOS.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorReincidenciaTecnicoQnt(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.REINCIDENCIA_EQUIPAMENTO_PERCENT.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorEquipReincidentes(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.PECAS_FILIAL.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorPecasFaltantesFiliais(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.PECAS_TOP_CINCO_MAIS_FALTANTES.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorCincoPecasMaisFaltantes(chamadosDoDia));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.PECAS_CRITICAS_MAIS_FALTANTES.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorPecasCriticasFaltantes(chamadosDoDia, parameters.Agrupador));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.PECAS_NOVAS_CADASTRADAS.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorPecasCriticasFaltantes(chamadosDoDia, parameters.Agrupador));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.PECAS_NOVAS_LIBERADAS.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorPecasCriticasFaltantes(chamadosDoDia, parameters.Agrupador));
                //}
                //else if (nomeIndicador == NomeIndicadorEnum.PERFORMANCE_FILIALS.Description())
                //{
                //    dadosDB.AddRange(ObterIndicadorSLAFilial(chamadosDoDia));
                //    dadosDB.AddRange(ObterIndicadorPendenciaFilial(chamadosDoDia));
                //    dadosDB.AddRange(ObterIndicadorReincidenciaFilial(chamadosDoDia));
                //    dadosDB.AddRange(ObterIndicadorSPAFilial(chamadosDoDia));
                //}

                dadosDB.AddRange(ObterIndicadorSLAFilial(chamados));
                dadosDB.AddRange(ObterIndicadorPendenciaFilial(chamados));
                dadosDB.AddRange(ObterIndicadorReincidenciaFilial(chamados));
                dadosDB.AddRange(ObterIndicadorSPAFilial(chamados));

                _dashboardService.Atualizar(nomeIndicador, dadosDB, new DateTime());
            //}
        }

        /// <summary>
        /// Calculo sempre é feito num prazo de 30 dias
        /// </summary>
        /// <param name="nomeIndicador"></param>
        /// <param name="parameters"></param>
        private void AtualizaIndicadoresDisponibilidadeTecnicosDashboard(string nomeIndicador, IndicadorParameters parameters)
        {
            TecnicoParameters tecnicoParameters = new()
            {
                Include = TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
                Tipo = TecnicoTipoEnum.DISPONIBILIDADE_TECNICOS,
                FilterType = TecnicoFilterEnum.FILTER_TECNICO_OS,
                PeriodoMediaAtendInicio = parameters.DataFim.AddDays(-30),
                PeriodoMediaAtendFim = parameters.DataFim
            };

            IQueryable<Tecnico> tecnicos = _tecnicosRepo.ObterQuery(tecnicoParameters);
            List<DashboardTecnicoDisponibilidadeTecnicoViewModel> dadosTecnicos = ObterDadosDashboardTecnicoDisponibilidade(tecnicos, tecnicoParameters);
            _dashboardService.Atualizar(nomeIndicador, dadosTecnicos, parameters.DataFim);
        }

        /// <summary>
        /// Atualiza todos os indicadores em um período
        /// </summary>
        /// <param name="periodoInicio">Período de início</param>
        /// <param name="periodoFim">Período de fim</param>
        public void AtualizaDadosIndicadoresDashboard(DateTime periodoInicio, DateTime periodoFim)
        {
            IndicadorParameters parameters = new() { DataInicio = periodoInicio, DataFim = periodoFim };

          //  this.AtualizaIndicadoresDisponibilidadeTecnicosDashboard(NomeIndicadorEnum.DISPONIBILIDADE_TECNICOS.Description(), parameters);

            foreach (NomeIndicadorEnum indicador in Enum.GetValues(typeof(NomeIndicadorEnum)))
            {
                switch (indicador)
                {
                    case NomeIndicadorEnum.PERFORMANCE_FILIALS:
                        parameters.Tipo = IndicadorTipoEnum.PERFORMANCE_FILIAIS;
                        parameters.Agrupador = IndicadorAgrupadorEnum.FILIAL;
                        parameters.Include = OrdemServicoIncludeEnum.OS_RAT_FILIAL_PRAZOS_ATENDIMENTO;
                        parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                        break;
                    case NomeIndicadorEnum.SLA_FILIAL:
                       parameters.Tipo = IndicadorTipoEnum.SLA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.FILIAL;
                       parameters.Include = OrdemServicoIncludeEnum.OS_RAT_FILIAL_PRAZOS_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.SLA_CLIENTE:
                       parameters.Tipo = IndicadorTipoEnum.SLA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.CLIENTE;
                       parameters.Include = OrdemServicoIncludeEnum.OS_RAT_CLIENTE_PRAZOS_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.SPA_CLIENTE:
                       parameters.Tipo = IndicadorTipoEnum.SPA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.CLIENTE;
                       parameters.Include = OrdemServicoIncludeEnum.OS_RAT_CLIENTE_PRAZOS_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.SPA_FILIAL:
                       parameters.Tipo = IndicadorTipoEnum.SPA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.FILIAL;
                       parameters.Include = OrdemServicoIncludeEnum.OS_RAT_FILIAL_PRAZOS_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.SPA_TECNICO_PERCENT:
                       parameters.Tipo = IndicadorTipoEnum.SPA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.TECNICO_PERCENT_SPA;
                       parameters.Include = OrdemServicoIncludeEnum.OS_TECNICO_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.SPA_TECNICO_QNT_CHAMADOS:
                       parameters.Tipo = IndicadorTipoEnum.SPA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.TECNICO_QNT_CHAMADOS_SPA;
                       parameters.Include = OrdemServicoIncludeEnum.OS_TECNICO_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.PENDENCIA_CLIENTE:
                       parameters.Tipo = IndicadorTipoEnum.PENDENCIA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.CLIENTE;
                       parameters.Include = OrdemServicoIncludeEnum.OS_RAT_CLIENTE_PRAZOS_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.PENDENCIA_FILIAL:
                       parameters.Tipo = IndicadorTipoEnum.PENDENCIA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.FILIAL;
                       parameters.Include = OrdemServicoIncludeEnum.OS_RAT_FILIAL_PRAZOS_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.PENDENCIA_TECNICO_PERCENT:
                       parameters.Tipo = IndicadorTipoEnum.PENDENCIA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.TECNICO_PERCENT_PENDENTES;
                       parameters.Include = OrdemServicoIncludeEnum.OS_TECNICO_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.PENDENCIA_TECNICO_QNT_CHAMADOS:
                       parameters.Tipo = IndicadorTipoEnum.PENDENCIA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.TECNICO_QNT_CHAMADOS_PENDENTES;
                       parameters.Include = OrdemServicoIncludeEnum.OS_TECNICO_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.REINCIDENCIA_CLIENTE:
                       parameters.Tipo = IndicadorTipoEnum.REINCIDENCIA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.CLIENTE;
                       parameters.Include = OrdemServicoIncludeEnum.OS_RAT_CLIENTE_PRAZOS_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.REINCIDENCIA_FILIAL:
                       parameters.Tipo = IndicadorTipoEnum.REINCIDENCIA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.FILIAL;
                       parameters.Include = OrdemServicoIncludeEnum.OS_RAT_FILIAL_PRAZOS_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.REINCIDENCIA_TECNICO_PERCENT:
                       parameters.Tipo = IndicadorTipoEnum.REINCIDENCIA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.TECNICO_PERCENT_REINCIDENTES;
                       parameters.Include = OrdemServicoIncludeEnum.OS_TECNICO_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.REINCIDENCIA_TECNICO_QNT_CHAMADOS:
                       parameters.Tipo = IndicadorTipoEnum.REINCIDENCIA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.TECNICO_QNT_CHAMADOS_REINCIDENTES;
                       parameters.Include = OrdemServicoIncludeEnum.OS_TECNICO_ATENDIMENTO;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.REINCIDENCIA_EQUIPAMENTO_PERCENT:
                       parameters.Tipo = IndicadorTipoEnum.REINCIDENCIA;
                       parameters.Agrupador = IndicadorAgrupadorEnum.EQUIPAMENTO_PERCENT_REINCIDENTES;
                       parameters.Include = OrdemServicoIncludeEnum.OS_EQUIPAMENTOS_ATENDIMENTOS;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_INDICADOR;
                       break;
                    case NomeIndicadorEnum.PECAS_FILIAL:
                       parameters.Tipo = IndicadorTipoEnum.PECA_FALTANTE;
                       parameters.Agrupador = IndicadorAgrupadorEnum.FILIAL;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES;
                       parameters.Include = OrdemServicoIncludeEnum.OS_PECAS;
                       break;
                    case NomeIndicadorEnum.PECAS_TOP_CINCO_MAIS_FALTANTES:
                       parameters.Tipo = IndicadorTipoEnum.PECA_FALTANTE;
                       parameters.Agrupador = IndicadorAgrupadorEnum.TOP_CINCO_PECAS_MAIS_FALTANTES;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES;
                       parameters.Include = OrdemServicoIncludeEnum.OS_PECAS;
                       break;
                    case NomeIndicadorEnum.PECAS_CRITICAS_MAIS_FALTANTES:
                       parameters.Tipo = IndicadorTipoEnum.PECA_FALTANTE;
                       parameters.Agrupador = IndicadorAgrupadorEnum.FILIAL;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES;
                       parameters.Include = OrdemServicoIncludeEnum.OS_PECAS;
                       break;
                    case NomeIndicadorEnum.PECAS_NOVAS_CADASTRADAS:
                       parameters.Tipo = IndicadorTipoEnum.PECA_FALTANTE;
                       parameters.Agrupador = IndicadorAgrupadorEnum.NOVAS_CADASTRADAS;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES;
                       parameters.Include = OrdemServicoIncludeEnum.OS_PECAS;
                       break;
                    case NomeIndicadorEnum.PECAS_NOVAS_LIBERADAS:
                       parameters.Tipo = IndicadorTipoEnum.PECA_FALTANTE;
                       parameters.Agrupador = IndicadorAgrupadorEnum.NOVAS_LIBERADAS;
                       parameters.FilterType = OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES;
                       parameters.Include = OrdemServicoIncludeEnum.OS_PECAS;
                       break;
                    default:
                        continue;
                }

                AtualizaIndicador(indicador.Description(), parameters);
            }
        }
    }
}
