using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void CriarFilaTasks(List<SatTaskTipo> tipos)
        {
            _logger.Info(MsgConst.INI_PROC_FILA);

            try
            {
                foreach (var tipo in tipos)
                {
                    SatTask task = (SatTask)_taskService
                        .ObterPorParametros(new SatTaskParameters { 
                            CodSatTaskTipo = tipo.CodSatTaskTipo
                        })
                        .Items?
                        .FirstOrDefault()!;

                    if (deveCriar(task, tipo))
                    {
                        var nTask = _taskService.Criar(new SatTask
                        {
                            Status = SatTaskStatusConst.PENDENTE,
                            DataHoraCad = DateTime.Now,
                            CodSatTaskTipo = tipo.CodSatTaskTipo
                        });

                        _logger.Info($"{ MsgConst.TASK_CRIADA }, { nTask.Tipo.Nome }");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{Constants.SISTEMA_CAMADA_TASKS} {ex.Message}");
            }

            _logger.Info(MsgConst.FIN_PROC_FILA);
        }

        private bool deveCriar(SatTask task, SatTaskTipo tipo)
        {
            var dataHrAtual = DateTime.Now;
            var daHrProc = task.DataHoraProcessamento!.Value;
            var dataHrProcTipo = daHrProc.AddMinutes(task.Tipo.TempoRepeticaoMinutos);
            var tempoAtual = DateTime.Now.TimeOfDay;

            return dataHrProcTipo < dataHrAtual && tempoAtual >= tipo.Inicio && tempoAtual <= tipo.Fim;
        }

        private async Task ProcessarFilaTasksAsync(IEnumerable<SatTask> tasks)
        {
            _logger.Info(MsgConst.INI_PROC_TASKS);

            try
            {
                var chamados = (IEnumerable<OrdemServico>)_osService.ObterPorParametros(new OrdemServicoParameters
                {
                    DataHoraManutInicio = DateTime.Now.AddMinutes(-5),
                    DataHoraManutFim = DateTime.Now
                }).Items;

                foreach (var task in tasks)
                {
                    if (task.Status == SatTaskStatusConst.PENDENTE)
                        continue;

                    task.DataHoraProcessamento = DateTime.Now;
                    task.Status = SatTaskStatusConst.PROCESSADO;
                    _taskService.Atualizar(task);

                    switch (task.CodSatTaskTipo)
                    {
                        case (int)SatTaskTipoEnum.INT_BB:
                            ExecutarBB(task);

                            continue;
                        case (int)SatTaskTipoEnum.INT_BANRISUL:
                            await ExecutarBanrisulAsync(task);

                            continue;
                        case (int)SatTaskTipoEnum.INT_ZAFFARI:
                            await ExecutarZaffariAsync(task, chamados);

                            continue;
                        case (int)SatTaskTipoEnum.INT_MRP:
                            ExecutarMRP(task);

                            continue;
                        case (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO:
                            ExecutarModelos(task);

                            continue;
                        case (int)SatTaskTipoEnum.ANS:
                            ExecutarANS(task, chamados);

                            continue;
                        case (int)SatTaskTipoEnum.BRB:
                            ExecutarBRB(task);

                            continue;
                        case (int)SatTaskTipoEnum.COBRA:
                            ExecutarCobra(task);

                            continue;

                        case (int)SatTaskTipoEnum.SICOOB:
                            ExecutarSicoob(task);

                            continue;
                        case (int)SatTaskTipoEnum.SICREDI:
                            ExecutarSicredi(task);

                            continue;
                        case (int)SatTaskTipoEnum.TROUBLESHOOTING:
                            ExecutarTroubleShooting(task);

                            continue;
                        case (int)SatTaskTipoEnum.TICKET_LOG:
                            ExecutarTicketLog(task);

                            continue;
                        case (int)SatTaskTipoEnum.SENIOR:
                            ExecutarSenior(task);

                            continue;
                        case (int)SatTaskTipoEnum.PROTEGE:
                            ExecutarProtege(task);

                            continue;
                        case (int)SatTaskTipoEnum.METRO_SP:
                            ExecutarMetroSP(task);

                            continue;
                        default:
                            _logger.Error($"{task.Tipo.Nome} nao registrado");

                            continue;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{Constants.SISTEMA_CAMADA_TASKS} {ex.Message}");
            }
        }
    }
}