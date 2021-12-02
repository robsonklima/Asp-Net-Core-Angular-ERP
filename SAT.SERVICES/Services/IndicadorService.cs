using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.MODELS.Extensions;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        private readonly IOrdemServicoRepository _osRepository;
        private readonly IDashboardService _dashboardService;
        private readonly IDispBBCriticidadeRepository _dispBBCriticidadeRepository;
        private readonly IFeriadoService _feriadoService;
        private readonly IEquipamentoContratoRepository _equipamentoContratoRepository;
        private readonly IDispBBRegiaoFilialRepository _dispBBRegiaoFilialRepository;
        private readonly IDispBBPercRegiaoRepository _dispBBPercRegiaoRepository;
        private readonly IDispBBDesvioRepository _dispBBDesvioRepository;
        private readonly ITecnicoRepository _tecnicosRepo;

        public IndicadorService(IOrdemServicoRepository osRepository,
            IFeriadoService feriadoService,
            IEquipamentoContratoRepository equipamentoContratoRepository,
            IDispBBRegiaoFilialRepository dispBBRegiaoFilialRepository,
            IDispBBCriticidadeRepository dispBBCriticidadeRepository,
            IDispBBPercRegiaoRepository dispBBPercRegiaoRepository,
            IDispBBDesvioRepository dispBBDesvioRepository,
            IDashboardService dashboardService,
             ITecnicoRepository tecnicosRepo)
        {
            _osRepository = osRepository;
            _feriadoService = feriadoService;
            _dispBBCriticidadeRepository = dispBBCriticidadeRepository;
            _equipamentoContratoRepository = equipamentoContratoRepository;
            _dispBBRegiaoFilialRepository = dispBBRegiaoFilialRepository;
            _dispBBPercRegiaoRepository = dispBBPercRegiaoRepository;
            _dispBBDesvioRepository = dispBBDesvioRepository;
            _dashboardService = dashboardService;
            _tecnicosRepo = tecnicosRepo;
        }

        private IEnumerable<OrdemServico> ObterOrdensServico(IndicadorParameters parameters)
        {
            return _osRepository.ObterQuery(new OrdemServicoParameters()
            {
                DataAberturaInicio = parameters.DataInicio,
                DataAberturaFim = parameters.DataFim,
                CodClientes = parameters.CodClientes,
                CodFiliais = parameters.CodFiliais,
                CodStatusServicos = parameters.CodStatusServicos,
                CodTiposIntervencao = parameters.CodTiposIntervencao,
                CodAutorizadas = parameters.CodAutorizadas,
                CodTiposGrupo = parameters.CodTiposGrupo,
                Include = parameters.Include,
                PageSize = Int32.MaxValue,
                FilterType = parameters.FilterType
            }).Where(w => w.PrazosAtendimento.Count > 0);
        }

        public List<DashboardTecnicoDisponibilidadeTecnicoViewModel> ObterDadosDisponibilidadeTecnicos(IndicadorParameters parameters)
        {
            return ObterIndicadorDisponibilidadeTecnicos(parameters);
        }

        public List<Indicador> ObterIndicadores(IndicadorParameters parameters)
        {
            // Força a atualização da tabela enquanto o projeto do Agendador não fica pronto - basta descomentar se quiser atualizar os dados do dia
            // AtualizaGeral();

            switch (parameters.Tipo)
            {
                case IndicadorTipoEnum.ORDEM_SERVICO:
                    return ObterIndicadorOrdemServico(parameters);
                case IndicadorTipoEnum.SLA:
                    return ObterIndicadorSLA(parameters);
                case IndicadorTipoEnum.SPA:
                    return ObterIndicadorSPA(parameters);
                case IndicadorTipoEnum.PENDENCIA:
                    return ObterIndicadorPendencia(parameters);
                case IndicadorTipoEnum.REINCIDENCIA:
                    return ObterIndicadorReincidencia(parameters);
                case IndicadorTipoEnum.PECA_FALTANTE:
                    return ObterIndicadorPecaFaltante(parameters);
                case IndicadorTipoEnum.DISPONIBILIDADE: // Ver depois o BB 
                    return ObterIndicadorDisponibilidade(parameters);
                case IndicadorTipoEnum.PERFORMANCE_FILIAIS:
                    return ObterDadosIndicador(parameters);
                default:
                    return null;
            }
        }

        public void AtualizaIndicadoresDashboard(string nomeIndicador, IndicadorParameters parameters)
        {
            DateTime dataInicio = parameters.DataInicio;
            DateTime dataFim = parameters.DataFim;

            List<OrdemServico> chamados = ObterOrdensServico(parameters).ToList();

            int diferencaDias = (int)dataFim.Subtract(dataInicio).TotalDays;

            for (int i = 0; i <= diferencaDias; i++)
            {
                DateTime data = dataInicio.AddDays(i);
                List<OrdemServico> chamadosDoDia = chamados.Where(s => s.DataHoraAberturaOS.Value.Date == data.Date).ToList();

                if (!chamadosDoDia.Any()) continue;

                List<Indicador> dadosDB = new();

                if (nomeIndicador == NomeIndicadorEnum.SLA_FILIAL.Description())
                {
                    dadosDB.AddRange(ObterIndicadorSLAFilial(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.SLA_CLIENTE.Description())
                {
                    dadosDB.AddRange(ObterIndicadorSLACliente(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.SPA_CLIENTE.Description())
                {
                    dadosDB.AddRange(ObterIndicadorSPACliente(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.SPA_FILIAL.Description())
                {
                    dadosDB.AddRange(ObterIndicadorSPAFilial(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.SPA_TECNICO_PERCENT.Description())
                {
                    dadosDB.AddRange(ObterIndicadorSPATecnicoPercent(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.SPA_TECNICO_QNT_CHAMADOS.Description())
                {
                    dadosDB.AddRange(ObterIndicadorSPATecnicoQnt(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.PENDENCIA_CLIENTE.Description())
                {
                    dadosDB.AddRange(ObterIndicadorPendenciaCliente(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.PENDENCIA_FILIAL.Description())
                {
                    dadosDB.AddRange(ObterIndicadorPendenciaFilial(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.PENDENCIA_TECNICO_PERCENT.Description())
                {
                    dadosDB.AddRange(ObterIndicadorPendenciaTecnicoPercent(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.PENDENCIA_TECNICO_QNT_CHAMADOS.Description())
                {
                    dadosDB.AddRange(ObterIndicadorPendenciaTecnicoQnt(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.REINCIDENCIA_CLIENTE.Description())
                {
                    dadosDB.AddRange(ObterIndicadorReincidenciaCliente(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.REINCIDENCIA_FILIAL.Description())
                {
                    dadosDB.AddRange(ObterIndicadorReincidenciaFilial(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.REINCIDENCIA_TECNICO_PERCENT.Description())
                {
                    dadosDB.AddRange(ObterIndicadorReincidenciaTecnicoPercent(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.REINCIDENCIA_TECNICO_QNT_CHAMADOS.Description())
                {
                    dadosDB.AddRange(ObterIndicadorReincidenciaTecnicoQnt(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.REINCIDENCIA_EQUIPAMENTO_PERCENT.Description())
                {
                    dadosDB.AddRange(ObterIndicadorEquipReincidentes(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.PECAS_FILIAL.Description())
                {
                    dadosDB.AddRange(ObterIndicadorPecasFaltantesFiliais(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.PECAS_TOP_MAIS_FALTANTES.Description())
                {
                    dadosDB.AddRange(ObterIndicadorTopPecaFaltante(chamadosDoDia, parameters.Agrupador));
                }
                else if (nomeIndicador == NomeIndicadorEnum.PECAS_MAIS_FALTANTES.Description())
                {
                    dadosDB.AddRange(ObterIndicadorPecasMaisFaltantesFiliais(chamadosDoDia));
                }
                else if (nomeIndicador == NomeIndicadorEnum.PECAS_NOVAS_CADASTRADAS.Description())
                {
                    dadosDB.AddRange(ObterIndicadorTopPecaFaltante(chamadosDoDia, parameters.Agrupador));
                }
                else if (nomeIndicador == NomeIndicadorEnum.PECAS_NOVAS_LIBERADAS.Description())
                {
                    dadosDB.AddRange(ObterIndicadorTopPecaFaltante(chamadosDoDia, parameters.Agrupador));
                }
                else if (nomeIndicador == NomeIndicadorEnum.PERFORMANCE_FILIALS.Description())
                {
                    dadosDB.AddRange(ObterIndicadorSLAFilial(chamadosDoDia));
                    dadosDB.AddRange(ObterIndicadorPendenciaFilial(chamadosDoDia));
                    dadosDB.AddRange(ObterIndicadorReincidenciaFilial(chamadosDoDia));
                    dadosDB.AddRange(ObterIndicadorSPAFilial(chamadosDoDia));
                }

                _dashboardService.Criar(nomeIndicador, dadosDB, data);
            }
        }


        // Enquanto o projeto do Agendador não fica pronto
        public void AtualizaIndicadoresDisponibilidadeTecnicosDashboard(string nomeIndicador, IndicadorParameters parameters)
        {
            TecnicoParameters tecnicoParameters = new()
            {
                Include = TecnicoIncludeEnum.TECNICO_ORDENS_SERVICO,
                Tipo = TecnicoTipoEnum.DISPONIBILIDADE_TECNICOS,
                FilterType = TecnicoFilterEnum.FILTER_TECNICO_OS
            };

            List<Tecnico> query = _tecnicosRepo.ObterQuery(tecnicoParameters).ToList();

            List<DashboardTecnicoDisponibilidadeTecnicoViewModel> listaDashboardTecnicos =
                ObterDadosDashboardTecnicoDisponibilidade(query, tecnicoParameters).ToList();

            _dashboardService.Criar(NomeIndicadorEnum.DISPONIBILIDADE_TECNICOS.Description(), listaDashboardTecnicos, DateTime.Now.Date);
        }

        // Enquanto o projeto do Agendador não fica pronto
        private void AtualizaGeral()
        {
            IndicadorParameters parameters = new()
            {
                DataInicio = DateTime.Now,// new DateTime(2021, 08, 01), -- se quiser puxar um periodo maior de tempo
                DataFim = DateTime.Now
            };

            AtualizaIndicadoresDisponibilidadeTecnicosDashboard(NomeIndicadorEnum.DISPONIBILIDADE_TECNICOS.Description(), parameters);

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
                    case NomeIndicadorEnum.PECAS_TOP_MAIS_FALTANTES:
                        parameters.Tipo = IndicadorTipoEnum.PECA_FALTANTE;
                        parameters.Agrupador = IndicadorAgrupadorEnum.TOP_PECAS_MAIS_FALTANTES;
                        parameters.FilterType = OrdemServicoFilterEnum.FILTER_PECAS_FALTANTES;
                        parameters.Include = OrdemServicoIncludeEnum.OS_PECAS;
                        break;
                    case NomeIndicadorEnum.PECAS_MAIS_FALTANTES:
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

                AtualizaIndicadoresDashboard(indicador.Description(), parameters);
            }

        }
    }
}
