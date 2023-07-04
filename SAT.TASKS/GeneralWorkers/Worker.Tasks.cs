using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.UTILS;

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

                    if (task.DataHoraProcessamento!.Value.AddMinutes(tipo.TempoRepeticaoMinutos) < DateTime.Now && tipo.Inicio <= DateTime.Now.TimeOfDay && tipo.Fim >= DateTime.Now.TimeOfDay)
                    {
                        var novaTask = _taskService.Criar(new SatTask
                        {
                            Status = SatTaskStatusConst.PENDENTE,
                            DataHoraCad = DateTime.Now,
                            CodSatTaskTipo = tipo.CodSatTaskTipo
                        });

                        _logger.Info($"{MsgConst.TASK_CRIADA}, {novaTask.Tipo.Nome}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{Constants.SISTEMA_CAMADA_TASKS} {ex.Message}");
            }

            _logger.Info(MsgConst.FIN_PROC_FILA);
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

                    AtualizarTask(task);

                    switch (task.CodSatTaskTipo)
                    {
                        case (int)SatTaskTipoEnum.INT_BB:
                            IntegrarBB(task);

                            continue;
                        case (int)SatTaskTipoEnum.INT_BANRISUL:
                            await IntegrarBanrisulAsync(task);

                            continue;
                        case (int)SatTaskTipoEnum.INT_ZAFFARI:
                            await IntegrarZaffariAsync(task, chamados);

                            continue;
                        case (int)SatTaskTipoEnum.INT_MRP:
                            IntegrarMRP(task);

                            continue;
                        case (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO:
                            IntegrarModelos(task);

                            continue;
                        case (int)SatTaskTipoEnum.ANS:
                            IntegrarANS(task, chamados);

                            continue;
                        case (int)SatTaskTipoEnum.BRB:
                            IntegrarBRB(task);

                            continue;
                        case (int)SatTaskTipoEnum.COBRA:
                            IntegrarCobra(task);

                            continue;

                        case (int)SatTaskTipoEnum.SICOOB:
                            IntegrarSicoob(task);

                            continue;
                        case (int)SatTaskTipoEnum.SICREDI:
                            IntegrarSicredi(task);

                            continue;
                        case (int)SatTaskTipoEnum.TROUBLESHOOTING:
                            IntegrarTroubleShooting(task);

                            continue;
                        case (int)SatTaskTipoEnum.TICKET_LOG:
                            IntegrarTicketLog(task);

                            continue;
                        case (int)SatTaskTipoEnum.SENIOR:
                            IntegrarSenior(task);

                            continue;
                        case (int)SatTaskTipoEnum.PROTEGE:
                            IntegrarProtege(task);

                            continue;
                        case (int)SatTaskTipoEnum.METRO_SP:
                            IntegrarMetroSP(task);

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

        private void AtualizarTask(SatTask task)
        {
            task.DataHoraProcessamento = DateTime.Now;
            task.Status = SatTaskStatusConst.PROCESSADO;

            _taskService.Atualizar(task);
        }
    }
}