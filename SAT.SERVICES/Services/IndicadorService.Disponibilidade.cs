using System;
using System.Collections.Generic;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using SAT.SERVICES.Interfaces;
using System.Linq;
using SAT.MODELS.Extensions;
using SAT.MODELS.Models;

namespace SAT.SERVICES.Services
{
    public partial class IndicadorService : IIndicadorService
    {
        public List<Indicador> ObterIndicadorDisponibilidade(IndicadorParameters parameters)
        {
            var chamados = ObterOrdensServicoDispBB(parameters);
            var equipamentosContratoAtivos = ObterEquipamentosContratoAtivosDispBB();

            switch (parameters.Agrupador)
            {
                case IndicadorAgrupadorEnum.REGIAO:
                    return ObterIndicadorDisponibilidadeRegiao(parameters, chamados, equipamentosContratoAtivos);
                default:
                    throw new NotImplementedException("Não Implementado");
            }
        }

        private List<Indicador> ObterIndicadorDisponibilidadeRegiao(IndicadorParameters parameters,
                            IEnumerable<OrdemServico> chamados, IEnumerable<EquipamentoContrato> equipamentosContratoAtivos)
        {
            var criticidades = ObterCriticidades();
            var percCriticidades = ObterPercCriticidadeRegioes();
            var desvios = ObterDesvios();
            var dispRegiao = InicializarModeloRegioes(criticidades);
            var regioesFiliaisBB = ObterRegioesFiliaisBB();
            var feriados = ObterFeriadosDoPeriodo(parameters.DataInicio);

            // Agrupa por filial
            equipamentosContratoAtivos.GroupBy(c => c.Filial).ToList().ForEach(equipamentosPorFilial =>
            {
                var filial = regioesFiliaisBB
                    .FirstOrDefault(f => f.CodFilial == equipamentosPorFilial.Key.CodFilial);

                var codUF = equipamentosPorFilial.FirstOrDefault().RegiaoAutorizada.Cidade.CodUF;

                // Agrupa por SLA. 
                // SLA <-> Criticidade 1 <-> 1. 
                //Todos os equipamentos com o mesmo SLA possuem a mesma criticidade.
                equipamentosPorFilial.ToList().GroupBy(e => e.CodSLA).ToList().ForEach(equipPorSLA =>
                {
                    var criticidade = criticidades
                       .FirstOrDefault(c => c.CodSLA == equipPorSLA.Key);

                    if (criticidade == null) return;

                    var qtdEquip = equipPorSLA.Count();
                    var minutosDisponiveisPeloSLA = ObterMinutosDisponiveis(parameters.DataInicio, codUF, criticidade, feriados);

                    var reg = dispRegiao.FirstOrDefault(r => r.CodRegiao.ToString() == filial.CodDispBBRegiao.Trim()
                        && r.CodCriticidade == criticidade.CodDispBBCriticidade);

                    reg.MinDispTotal += (minutosDisponiveisPeloSLA * qtdEquip);
                    reg.QTdr += qtdEquip;

                    equipPorSLA.ToList()
                               .ForEach(equip => reg.ReceitaTotal += equip.ValorReceita);

                    chamados.Where(c => c.CodFilial == filial.CodFilial &&
                        c.EquipamentoContrato.CodSLA == equipPorSLA.Key)
                        .ToList().ForEach(c =>
                        {
                            reg.MinIndispTotal += ObterMinutosIndisponiveis(parameters.DataInicio, criticidade, c, feriados);
                            reg.QtdOS++;
                        });
                });
            });

            // DeltaMD e PercRebate
            dispRegiao.Where(dr => dr.MinIndispTotal > 0).ToList().ForEach(dr =>
            {
                var percRegCriticidade = percCriticidades.FirstOrDefault(dpr => dpr.CodDispBBRegiao == dr.CodRegiao &&
                                                           dpr.Criticidade == dr.CodCriticidade);

                dr.MetaRegiaoCriticidade = Convert.ToDouble(percRegCriticidade.Percentual);
                dr.Desvio = DesvioDeAtingimentoMetaIndisponibilidade(dr.IndCr, percRegCriticidade);

                if (dr.IndCr > dr.MetaRegiaoCriticidade)
                {
                    decimal deltaMD = Convert.ToDecimal(Math.Abs(dr.Desvio));

                    var desvioTabelado = desvios.FirstOrDefault(d =>
                        deltaMD >= Math.Abs(d.ValInicial) && deltaMD <= Math.Abs(d.ValFinal));

                    if (desvioTabelado != null)
                    {
                        dr.DesvioTabelado = desvioTabelado.Percentual;
                        dr.Rebate = CalculaRebate(dr.ReceitaTotal, desvioTabelado);
                    }
                }
            });

            var temp = dispRegiao.Where(dr => dr.QtdOS > 0);
            var temp2 = dispRegiao.Where(dr => dr.QtdOS == 0);
            var temp3 = dispRegiao.Where(dr => dr.Rebate > 0);
            return new List<Indicador>();
        }

        private double ObterMinutosDisponiveis(DateTime dataReferencia, int codUF, DispBBCriticidade criticidade, IEnumerable<Feriado> feriados)
        {
            var diasUteisDoMes = dataReferencia.GetMonthWorkingDays(criticidade.IndSab.Value, criticidade.IndDom.Value);
            var primeiroDiaDoMes = dataReferencia.GetFirstDayOfMonth();
            var ultimoDiaDoMes = dataReferencia.GetLastDayOfMonth();

            // Se a criticidade considera os feriados estes não precisam ser calculados
            var nroFeriados = Convert.ToBoolean(criticidade.IndFeriado) ?
                    0 : _feriadoService.ObterNroFeriadosDoPeriodo(primeiroDiaDoMes, ultimoDiaDoMes, null, codUF, feriados);

            var diasContabilizados = diasUteisDoMes - nroFeriados;
            var minDisponiveis = (criticidade.HorarioFim - criticidade.HorarioInicio).Value.TotalMinutes * diasContabilizados;

            return minDisponiveis;
        }

        private double ObterMinutosIndisponiveis(DateTime dataReferencia, DispBBCriticidade criticidade, OrdemServico chamado, IEnumerable<Feriado> feriados)
        {
            var primeiroDiaDoMes = dataReferencia.GetFirstDayOfMonth();
            var minDisponiveisPorDia = (criticidade.HorarioFim - criticidade.HorarioInicio).Value.TotalMinutes;

            DateTime dataSolucao =
                !chamado.RelatoriosAtendimento.Any() ? DateTime.Now :
                chamado.RelatoriosAtendimento.OrderByDescending(p => p.CodRAT).Select(p => p.DataHoraSolucao).FirstOrDefault();

            DateTime dataAbertura =
                chamado.DataHoraAberturaOS.Value >= primeiroDiaDoMes ? chamado.DataHoraAberturaOS.Value : primeiroDiaDoMes;

            var diasAberta = DateTimeEx.GetWorkingDaysBetweenTwoDates(dataAbertura,
                                                                      dataSolucao, criticidade.IndSab.Value, criticidade.IndDom.Value);

            var nroFeriados = Convert.ToBoolean(criticidade.IndFeriado) ?
                    0 : _feriadoService.ObterNroFeriadosDoPeriodo(dataAbertura, dataSolucao, null,
                                                                chamado.LocalAtendimento.Cidade.CodUF, feriados);

            var diferencaAberturaEFechamento = DiferencaAberturaEFechamento(dataAbertura, dataSolucao, criticidade);

            var diasConsiderados = diasAberta - nroFeriados;
            var minIndisp = (diasConsiderados * minDisponiveisPorDia) - diferencaAberturaEFechamento;

            return minIndisp;
        }

        private double DiferencaAberturaEFechamento(DateTime dataAbertura, DateTime dataSolucao, DispBBCriticidade criticidade)
        {
            double minDiaInicial = 0, minDiaFinal = 0;

            if (dataAbertura.TimeOfDay >= criticidade.HorarioInicio &&
                    dataAbertura.TimeOfDay < criticidade.HorarioFim)
                minDiaInicial = (dataAbertura.TimeOfDay - criticidade.HorarioInicio).Value.TotalMinutes;

            if (dataSolucao > dataAbertura &&
                    dataSolucao.TimeOfDay >= criticidade.HorarioInicio &&
                        dataSolucao.TimeOfDay < criticidade.HorarioFim)
                minDiaFinal = (criticidade.HorarioFim - dataSolucao.TimeOfDay).Value.TotalMinutes;

            return minDiaFinal + minDiaInicial;
        }

        private IEnumerable<DispBBCriticidade> ObterCriticidades() =>
            _dispBBCriticidadeRepository.ObterPorParametros(new DispBBCriticidadeParameters
            {
                IndAtivo = 1,
                PageSize = Int32.MaxValue
            });

        private IEnumerable<Feriado> ObterFeriadosDoPeriodo(DateTime data) =>
            _feriadoService.ObterFeriadosDoPeriodo(data);

        private IEnumerable<OrdemServico> ObterOrdensServicoMesAtualDispBB(IndicadorParameters parameters)
        {
            return _osRepository.ObterPorParametros(new OrdemServicoParameters
            {
                DataInicioDispBB = parameters.DataInicio,
                DataFimDispBB = parameters.DataFim,
                CodClientes = ((int)ClienteEnum.BB).ToString(),
                CodContrato = (int)ContratoEnum.BB_TECNO_0125_2017,
                CodTiposIntervencao = "2,5,17,18,19,20",
                CodUsuarioCadastro = "SERVIÇO",
                CodEquipamentos = "408, 409, 387, 404, 402, 411, 403, 412, 443",
                IndServico = 1,
                PageSize = Int32.MaxValue
            });
        }

        private IEnumerable<OrdemServico> ObterOrdensServicoAntigasAbertasDispBB(IndicadorParameters parameters)
        {
            return _osRepository.ObterPorParametros(new OrdemServicoParameters
            {
                DataFimDispBB = parameters.DataFim,
                CodClientes = ((int)ClienteEnum.BB).ToString(),
                CodContrato = (int)ContratoEnum.BB_TECNO_0125_2017,
                CodTiposIntervencao = "2,5,17,18,19,20",
                CodUsuarioCadastro = "SERVIÇO",
                CodEquipamentos = "408, 409, 387, 404, 402, 411, 403, 412, 443",
                NotIn_CodStatusServicos = "2, 3",
                IndServico = 1,
                PageSize = Int32.MaxValue
            });
        }

        private IEnumerable<OrdemServico> ObterOrdensServicoDispBB(IndicadorParameters parameters)
        {
            var mesAtual = ObterOrdensServicoMesAtualDispBB(parameters);
            var anterioresAbertas = ObterOrdensServicoAntigasAbertasDispBB(parameters);

            return mesAtual.Union(anterioresAbertas);
        }

        private IEnumerable<EquipamentoContrato> ObterEquipamentosContratoAtivosDispBB()
        {
            return _equipamentoContratoRepository.ObterPorParametros(new EquipamentoContratoParameters
            {
                CodCliente = (int)ClienteEnum.BB,
                CodContrato = (int)ContratoEnum.BB_TECNO_0125_2017,
                CodEquipamentos = "408, 409, 387, 404, 402, 411, 403, 412, 443",
                IndAtivo = 1,
                PageSize = Int32.MaxValue
            });
        }

        private IEnumerable<DispBBRegiaoFilial> ObterRegioesFiliaisBB()
        {
            return _dispBBRegiaoFilialRepository.ObterPorParametros(new DispBBRegiaoFilialParameters
            {
                PageSize = Int32.MaxValue
            });
        }

        private IEnumerable<DispBBPercRegiao> ObterPercCriticidadeRegioes()
        {
            return _dispBBPercRegiaoRepository.ObterPorParametros(new DispBBPercRegiaoParameters
            {
                IndAtivo = 1,
                PageSize = Int32.MaxValue
            });
        }

        private IEnumerable<DispBBDesvio> ObterDesvios() =>
            _dispBBDesvioRepository.ObterPorParametros(new DispBBDesvioParameters
            {
                IndAtivo = 1,
                PageSize = Int32.MaxValue
            });

        private List<DispRegiaoBBModel> InicializarModeloRegioes(IEnumerable<DispBBCriticidade> criticidades)
        {
            List<DispRegiaoBBModel> dispRegiao = new List<DispRegiaoBBModel>();

            foreach (DispBBRegiaoEnum region in Enum.GetValues(typeof(DispBBRegiaoEnum)))
            {
                criticidades.ToList().ForEach(c =>
                {
                    dispRegiao.Add(new DispRegiaoBBModel
                    {
                        CodRegiao = (int)region,
                        CodCriticidade = c.CodDispBBCriticidade,
                        MinDispTotal = 0,
                        MinIndispTotal = 0,
                        QTdr = 0,
                        QtdOS = 0,
                        ReceitaTotal = 0,
                        Rebate = 0,
                        MetaRegiaoCriticidade = 0,
                        DesvioTabelado = 0
                    });
                });
            }
            return dispRegiao;
        }

        private double DesvioDeAtingimentoMetaIndisponibilidade(double indCr, DispBBPercRegiao perc)
        {
            var desvio = ((-indCr + 100) / (-Convert.ToDouble(perc.Percentual) + 100) * 100) - 100;

            if (desvio >= 0) return 0;
            return desvio;
        }

        private decimal CalculaRebate(decimal receitaTotal, DispBBDesvio desvio) =>
            receitaTotal * desvio.Percentual;
    }
}