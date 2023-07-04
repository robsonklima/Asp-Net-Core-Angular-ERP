using System.Collections.Generic;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System.Linq;
using System;
using SAT.MODELS.Entities.Constants;

namespace SAT.SERVICES.Services
{
    public class OrdemServicoAlertaService : IOrdemServicoAlertaService
    {
        private readonly IOrdemServicoRepository _ordemServicoRepo;
        private readonly ISequenciaRepository _sequenciaRepo;
        private readonly IDispBBCalcEquipamentoContratoRepository _dispBBCalcEquipContratoRepo;
        private readonly IDispBBPercRegiaoRepository _dispBBPercRegiaoRepo;

        public OrdemServicoAlertaService(
            IOrdemServicoRepository ordemServicoRepo,
            ISequenciaRepository sequenciaRepo,
            IDispBBCalcEquipamentoContratoRepository dispBBCalcEquipContratoRepo,
            IDispBBPercRegiaoRepository dispBBPercRegiaoRepo
        )
        {
            _ordemServicoRepo = ordemServicoRepo;
            _sequenciaRepo = sequenciaRepo;
            _dispBBCalcEquipContratoRepo = dispBBCalcEquipContratoRepo;
            _dispBBPercRegiaoRepo = dispBBPercRegiaoRepo;
        }

        public List<Alerta> ObterAlertas(OrdemServico os)
        {
            var alertas = new List<Alerta>();

            if (
                (os.CodUsuarioCadastro != null && os.CodUsuarioCadastro.Equals(Constants.USUARIO_SERVICO)) &&
                os.EquipamentoContrato.CodContrato.Equals(Constants.CONTRATO_BB_TECNOLOGIA) &&
                !Constants.EQUIPS_TDS_TCC_TOP_TR1150.Any(i => i.Equals(os.EquipamentoContrato.CodEquip)) &&
                Constants.TIPO_INTERVENCAO_GERAL.Any(i => i.Equals(os.CodTipoIntervencao))
                )
            {
                alertas = ObterAlertasDispBB(os, alertas);
            }

            alertas = ObterAvisoChamadoVisualizado(os, alertas);
            if (os.CodEquipContrato != null)
            {
                alertas = ObterAvisoChamadosMesmoEquip(os, alertas);
                alertas = ObterAvisoChamadosCidadePinpad(os, alertas);
                alertas = ObterAvisoBaterias(os, alertas);
                alertas = ObterAvisoTrancaReciclador(os, alertas);
                alertas = ObterAvisosPreventivaContratual(os, alertas);
            }

            return alertas;
        }

        private List<Alerta> ObterAvisosPreventivaContratual(OrdemServico os, List<Alerta> alertas)
        {
            if (os.CodCliente != (int)ClienteEnum.BANCO_DA_AMAZONIA)
                return alertas;

            var alerta = new Alerta
            {
                Titulo = "Preventivas Contratuais",
                Descricao = new List<string>(){"Verificar necessidade de aplicação de preventiva contratual."},
                Tipo = Constants.PRIMARY
            };

            alertas.Add(alerta);
            
            return alertas;
        }

        private List<Alerta> ObterAlertasDispBB(OrdemServico os, List<Alerta> listaAlerta)
        {
            var alerta = new Alerta
            {
                Titulo = "Chamado BB Tecnologia 0125/2017 ",
                Descricao = new List<string>(),
                Tipo = Constants.PRIMARY
            };
            var hrMaxPorEquip = CalculaHorasDisponiveis(os).ToString(@"hh\:mm");
            TimeSpan horasConsumidas = CalculaHorasConsumidas(os);

            alerta.Descricao.Add($@"Nesta criticidade {os.EquipamentoContrato.DispBBCriticidade.CodDispBBCriticidade} 
                                    e região {os.LocalAtendimento.Cidade.UnidadeFederativa.DispBBRegiaoUF.DispBBRegiao.Nome.ToUpper()}, 
                                    cada máquina deste parque pode consumir no máximo {hrMaxPorEquip} hrs e este chamado já consumiu {horasConsumidas.TotalHours} hrs. 
                                    É de suma importância priorizar este atendimento e avaliar criteriosamente se a causa foi vandalismo/mau uso.");

            listaAlerta.Add(alerta);
            return listaAlerta;
        }

        private TimeSpan CalculaHorasConsumidas(OrdemServico os)
        {
            var dataSolicitacao = os.DataHoraAberturaOS;
            var dataAtualOuFimRat = os.RelatoriosAtendimento.Any() ? os.RelatoriosAtendimento.LastOrDefault().DataHoraSolucao : DateTime.Now;

            var diasNaoUteis = 0;

            int minutes = (int)(dataAtualOuFimRat - dataSolicitacao).Value.TotalMinutes;

            TimeSpan inicioSla = os.EquipamentoContrato.AcordoNivelServico.HorarioInicio.Value.TimeOfDay;
            TimeSpan fim = os.EquipamentoContrato.AcordoNivelServico.HorarioFim.Value.TimeOfDay;

            if (diasNaoUteis > 0)
            {
                var minutosNaoUteis = fim.Subtract(inicioSla).TotalMinutes * diasNaoUteis;

                var minutosTotais = Enumerable.Range(0, minutes)
                    .Select(min => dataSolicitacao.Value.AddMinutes(min))
                    .Where(dt => dt.TimeOfDay >= inicioSla && dt.TimeOfDay < fim)
                    .Count() - (int)minutosNaoUteis;

                return TimeSpan.FromHours(Convert.ToDouble(minutosTotais / 60));
            }

            return TimeSpan
                        .FromHours(
                            Enumerable.Range(0, minutes)
                            .Select(min => dataSolicitacao.Value.AddMinutes(min))
                            .Where(dt => dt.TimeOfDay >= inicioSla && dt.TimeOfDay < fim)
                            .Count() / 60);
        }

        private TimeSpan CalculaHorasDisponiveis(OrdemServico os)
        {

            var dispBBParque = _dispBBCalcEquipContratoRepo
                        .ObterPorParametros(new DispBBCalcEquipamentoContratoParameters
                        {
                            CodDispBBRegiao = os.LocalAtendimento.Cidade.UnidadeFederativa.DispBBRegiaoUF.CodDispBBRegiao,
                            Criticidade = os.EquipamentoContrato.DispBBCriticidade.CodDispBBCriticidade,
                            AnoMes = DateTime.Now.ToString("yyyyMM")
                        });

            var dispBBPerc = _dispBBPercRegiaoRepo
                                .ObterPorParametros(new DispBBPercRegiaoParameters { IndAtivo = 1 })
                                .FirstOrDefault(e => e.Criticidade == os.EquipamentoContrato.DispBBCriticidade.CodDispBBCriticidade &&
                                            e.CodDispBBRegiao == os.LocalAtendimento.Cidade.UnidadeFederativa.DispBBRegiaoUF.CodDispBBRegiao);

            var hrMaxPorEquip = Convert.ToDouble(
                                    (dispBBParque.Sum(disp => disp.MinTotais) / 60) *
                                    (dispBBPerc.Percentual / 100) /
                                    (dispBBParque.Count() == 0 ? 1 : dispBBParque.Count())
                                    );

            return TimeSpan.FromHours(hrMaxPorEquip);
        }

        private List<Alerta> ObterAvisoChamadosCidadePinpad(OrdemServico os, List<Alerta> listaAlertas)
        {
            var osEquip = _ordemServicoRepo
                    .ObterPorParametros(new OrdemServicoParameters
                    {
                        Include = OrdemServicoIncludeEnum.OS_EQUIPAMENTOS,
                        CodEquipamentos = string.Join(",", Constants.EQUIPAMENTOS_PINPAD),
                        NotIn_CodStatusServicos = "2,3,16"
                    })
                    .Where(c => c.CodOS != os.CodOS &&
                            (c.LocalAtendimento != null && c.LocalAtendimento.CodCidade.Equals(os.LocalAtendimento.CodCidade)) &&
                            Constants.EQUIPAMENTOS_PINPAD.Any(i => i.Equals(c.CodEquip)))
                    .ToList();

            if (osEquip.Any())
            {
                var alerta = new Alerta
                {
                    Titulo = "Chamados de PINPAD para a mesma cidade ",
                    Descricao = new List<string>(),
                    Tipo = Constants.PRIMARY
                };

                osEquip.ForEach(e =>
                {
                    alerta.Descricao.Add($"{e.CodOS} - {e.TipoIntervencao.NomTipoIntervencao} - {e.StatusServico.NomeStatusServico}");
                });

                listaAlertas.Add(alerta);

                return listaAlertas;
            }

            return listaAlertas;
        }

        private List<Alerta> ObterAvisoChamadoVisualizado(OrdemServico os, List<Alerta> listaAlertas)
        {
            if (os.CodStatusServico != 8) return listaAlertas;

            var alerta = new Alerta
            {
                Titulo = "Chamado visualizado pelo técnico",
                Descricao = new List<string>(),
                Tipo = Constants.SUCCESS
            };

            if (string.IsNullOrEmpty(os.CodUsuarioOSMobileLida))
            {
                alerta.Titulo = "Chamado ainda não visualizado";
                alerta.Descricao.Add($"Técnico Transferido: {os.Tecnico?.Nome}");
                alerta.Tipo = Constants.PRIMARY;
                listaAlertas.Add(alerta);

                return listaAlertas;
            }

            alerta.Descricao.Add($"Técnico: {os.Tecnico?.Nome} às {os.DataHoraOSMobileLida}");

            listaAlertas.Add(alerta);

            return listaAlertas;
        }

        private List<Alerta> ObterAvisoChamadosMesmoEquip(OrdemServico os, List<Alerta> listaAlertas)
        {
            var osEquip = _ordemServicoRepo
                    .ObterPorParametros(new OrdemServicoParameters
                    {
                        CodEquipContrato = os.CodEquipContrato,
                        NotIn_CodStatusServicos = "2,3,16"
                    }).Where(c => c.CodOS != os.CodOS).ToList();

            if (!osEquip.Any() || !os.CodEquipContrato.HasValue) return listaAlertas;

            var alerta = new Alerta
            {
                Titulo = "Mais de um chamado aberto para este equipamento",
                Descricao = new List<string>(),
                Tipo = Constants.WARNING
            };

            osEquip.ForEach(os =>
                    {
                        alerta.Descricao.Add($"{os.CodOS} - {os.TipoIntervencao.NomTipoIntervencao} - {os.StatusServico.NomeStatusServico}");
                    });

            listaAlertas.Add(alerta);

            return listaAlertas;
        }

        private List<Alerta> ObterAvisoBaterias(OrdemServico os, List<Alerta> listaAlertas)
        {
            var gerarAlerta = false;

            switch (os.CodCliente)
            {
                case Constants.CLIENTE_BB:
                    if (os.CodEquip == Constants.TAART_290_02_309 ||
                        os.CodEquip == Constants.TAASF_290_02_047 ||
                        os.CodEquip == Constants.TAAMC_290_02_049)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_BANRISUL:
                    if (os.CodEquip == Constants.TMD_2100_290_02_037)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_SAFRA:
                    if (os.CodEquip == Constants.TMF_5110_290_02_303)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_CEF:
                    if (os.CodEquip == Constants.TS_3100F_290_01_552)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_SICOOB:
                    if (os.CodEquip == Constants.TMD_5100_290_01_980 ||
                        os.CodEquip == Constants.TMF_5100_290_01_979 ||
                        os.CodEquip == Constants.TMF_5100_290_01_604 ||
                        os.CodEquip == Constants.TMR_5150_290_02_061 ||
                        os.CodEquip == Constants.TMD_5100_290_02_025 ||
                        os.CodEquip == Constants.TMF_5100_290_02_349 ||
                        os.CodEquip == Constants.TMF_5110_290_01_976 ||
                        os.CodEquip == Constants.TMR_5160_290_02_363 ||
                        os.CodEquip == Constants.TAS_3100_290_02_252 ||
                        os.CodEquip == Constants.TMD_4100_290_01_834 ||
                        os.CodEquip == Constants.TMD_5100_290_02_346)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_SICREDI:
                    if (os.CodEquip == Constants.TMF_5100_290_01_975 ||
                        os.CodEquip == Constants.TMF_5100_290_01_898 ||
                        os.CodEquip == Constants.TMF_5100_290_02_402 ||
                        os.CodEquip == Constants.TC_3100_290_02_288  ||
                        os.CodEquip == Constants.TMF_4100ABNT_290_01_434)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_BANCO_DA_AMAZONIA:
                    if (os.CodEquip == Constants.TMF_4100_290_02_028 ||
                        os.CodEquip == Constants.TC_3100_290_02_071  ||
                        os.CodEquip == Constants.TMF_4100_290_01_863 ||
                        os.CodEquip == Constants.TMF_4100_290_01_776)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_BRB:
                    if (os.CodEquip == Constants.TMF_2100_290_01_906 ||
                        os.CodEquip == Constants.TMD_2100_290_01_904 ||
                        os.CodEquip == Constants.TS_2100_290_01_908)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_ITAU:
                    if (os.CodEquip == Constants.TMRSD_5100_290_02_323)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_BANESTES:
                    if (os.CodEquip == Constants.TMF_2100_290_02_057 ||
                        os.CodEquip == Constants.TS_2100_290_01_918  ||
                        os.CodEquip == Constants.TMF_2100_P_I_290_02_058)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_BANPARA:
                    if (os.CodEquip == Constants.TS_5150_290_01_942  ||
                        os.CodEquip == Constants.TPC_4110_290_01_941 ||
                        os.CodEquip == Constants.TMF_5100_290_01_940 ||
                        os.CodEquip == Constants.TS_5160_290_01_943)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_SAQUE_PAGUE:
                    if (os.CodEquip == Constants.TS_2100_290_02_322  ||
                        os.CodEquip == Constants.TMR_5100_290_02_326 ||
                        os.CodEquip == Constants.TMR_5160_290_02_062 ||
                        os.CodEquip == Constants.TPR_4111_290_01_946)
                    {
                        gerarAlerta = true;
                    }
                    break;
                default:
                    break;
            }

            if (gerarAlerta)
            {
                var alerta = new Alerta
                {
                    Titulo = "Alerta de Teclado - Verificar bateria",
                    Descricao = new List<string>(),
                    Tipo = Constants.WARNING
                };

                alerta.Descricao.Add("Revisar a bateria do teclado, se detectarmos baterias diferentes da marca TEKCELL devemos trocar para este fornecedor.");
                listaAlertas.Add(alerta);
            }
            return listaAlertas;
        }

        private List<Alerta> ObterAvisoTrancaReciclador(OrdemServico os, List<Alerta> listaAlertas)
        {
            var gerarAlerta = false;

            switch (os.CodCliente)
            {
                case Constants.CLIENTE_SICOOB:
                    if (
                        os.CodEquip == Constants.TMR_5150_290_02_061 ||
                        os.CodEquip == Constants.TMR_5160_290_02_363 ||
                        os.CodEquip == Constants.TMR_5160_290_02_363_LEGADO_INATIVO)
                    {
                        gerarAlerta = true;
                    }
                    break;
                case Constants.CLIENTE_BB:
                    if (
                        os.CodEquip == Constants.TAART_290_02_309)
                    {
                        gerarAlerta = true;
                    }
                    break;
                default:
                    break;
            }

            if (gerarAlerta)
            {
                var alerta = new Alerta
                {
                    Titulo = "Alerta de Funcionalidade - Tranca do Reciclador",
                    Descricao = new List<string>(),
                    Tipo = Constants.WARNING
                };

                alerta.Descricao.Add("Revisar a funcionalidade da tranca do reciclador inferior mediante diadnóstico de teste - HARDLOCK. e relatar no chamado essa ação");
                listaAlertas.Add(alerta);
            }
            return listaAlertas;
        }
    }
}