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
                        .ObterPorParametros(new SatTaskParameters { CodSatTaskTipo = tipo.CodSatTaskTipo })
                        .Items?
                        .FirstOrDefault()!;

                    var dCriar = deveCriar(task);

                    if (dCriar)
                    {
                        var novaTask = _taskService.Criar(new SatTask
                        {
                            Status = SatTaskStatusConst.PENDENTE,
                            DataHoraCad = DateTime.Now,
                            CodSatTaskTipo = tipo.CodSatTaskTipo
                        });

                        _logger.Info($"{ MsgConst.TASK_CRIADA }, { novaTask.Tipo.Nome }");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ Constants.SISTEMA_CAMADA_TASKS } { ex.Message }");
            }

            _logger.Info(MsgConst.FIN_PROC_FILA);
        }

        private bool deveCriar(SatTask task)
        {
            if (task is null)
                return true;

            if (task.Status == SatTaskStatusConst.PENDENTE)
                return false;

            return ObterPermissao(task);
        }

        private async Task ProcessarFilaTasksAsync(IEnumerable<SatTask> tasks)
        {
            _logger.Info(MsgConst.INI_PROC_TASKS);

            try
            {
                var chamados = ObterFilaChamados();
                
                foreach (var task in tasks)
                {
                    if (!deveProcessar(task))
                        continue;

                    AtualizarTask(task);

                    switch (task.CodSatTaskTipo)
                    {
                        case (int)SatTaskTipoEnum.INT_BB:
                            IntegrarBB(task);

                            break;
                        case (int)SatTaskTipoEnum.INT_BANRISUL:
                            await IntegrarBanrisulAsync(task);

                            break;
                        case (int)SatTaskTipoEnum.INT_ZAFFARI:
                            await IntegrarZaffariAsync(task, chamados);

                            break;
                        case (int)SatTaskTipoEnum.INT_MRP:
                            IntegrarMRP(task);

                            break;
                        case (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO:
                            IntegrarModelos(task);

                            break;
                        case (int)SatTaskTipoEnum.ANS:
                            IntegrarANS(task, chamados);

                            break;
                        default:
                            break; 
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ Constants.SISTEMA_CAMADA_TASKS } { ex.Message }");
            }
        }

        private void AtualizarTask(SatTask task)
        {
            task.DataHoraProcessamento = DateTime.Now;
            task.Status = SatTaskStatusConst.PROCESSADO;
            
            _taskService.Atualizar(task);
        }

        private bool deveProcessar(SatTask task) => ObterPermissao(task);

        private bool ObterPermissao(SatTask task)
        {
            DateTime dtHrProc = task.DataHoraProcessamento.HasValue ? 
                task.DataHoraProcessamento.Value : default(DateTime);

            switch (task.CodSatTaskTipo)
            {
                case (int)SatTaskTipoEnum.INT_BANRISUL:
                    return DataHelper.passouXMinutos(dtHrProc, (int)Constants.INT_BANR_T);
                case (int)SatTaskTipoEnum.INT_BB:
                    return DataHelper.passouXMinutos(dtHrProc, (int)Constants.INT_BB_T);
                case (int)SatTaskTipoEnum.INT_ZAFFARI:
                    return DataHelper.passouXMinutos(dtHrProc, (int)Constants.INT_ZAFF_T);
                case (int)SatTaskTipoEnum.INT_MRP:
                    return DataHelper.is2Horas() && DataHelper.passouXMinutos(dtHrProc, (int)Constants.INT_LOG_MRP_T);
                case (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO:
                    return DataHelper.is23Horas() && DataHelper.passouXMinutos(dtHrProc, (int)Constants.ATU_PAR_MOD_T);
                default:
                    return false;
            }
        }

        private IEnumerable<OrdemServico> ObterFilaChamados()
        {
            var parametros = new OrdemServicoParameters
            {
                DataHoraManutInicio = DateTime.Now.AddMinutes(-5),
                DataHoraManutFim = DateTime.Now
            };
            return (IEnumerable<OrdemServico>)_osService.ObterPorParametros(parametros).Items;
        }
    }
}