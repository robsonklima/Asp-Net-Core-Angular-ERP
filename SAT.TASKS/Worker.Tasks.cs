using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using SAT.UTILS;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void AtualizarFilaTasks(List<SatTaskTipo> tipos)
        {
            _logger.Info(MsgConst.INI_PROC_FILA);

            foreach (var tipo in tipos)
            {
                SatTask task = (SatTask)_taskService
                    .ObterPorParametros(new SatTaskParameters { CodSatTaskTipo = tipo.CodSatTaskTipo })
                    .Items?
                    .FirstOrDefault()!;

                if (deveCriarTask(task))
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

            _logger.Info(MsgConst.FIN_PROC_FILA);
        }

        private bool deveCriarTask(SatTask task)
        {
            if (task is null)
                return true;

            if (task.Status == SatTaskStatusConst.PENDENTE)
                return false;

            var dtHrProc = task.DataHoraProcessamento!.Value;

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

        private async Task ProcessarFilaTasksAsync(IEnumerable<SatTask> tasks)
        {
            _logger.Info(MsgConst.INI_PROC_TASKS);

            foreach (var task in tasks)
            {
                if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BB)
                {
                    _integracaoBBService.Processar();
                    task.DataHoraProcessamento = DateTime.Now;
                    task.Status = SatTaskStatusConst.PROCESSADO;
                    _taskService.Atualizar(task);

                    continue;
                }

                if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BANRISUL)
                {
                    await _integracaoBanrisulService.ProcessarEmailsAsync();
                    _integracaoBanrisulService.ProcessarRetornos();
                    task.DataHoraProcessamento = DateTime.Now;
                    task.Status = SatTaskStatusConst.PROCESSADO;
                    _taskService.Atualizar(task);

                    continue;
                }

                if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_ZAFFARI)
                {
                    var parametros = new OrdemServicoParameters
                    {
                        CodCliente = Constants.CLIENTE_ZAFFARI,
                        DataHoraManutInicio = DateTime.Now.AddMinutes(-5),
                        DataHoraManutFim = DateTime.Now,
                        IndServico = 1,
                        IndIntegracao = 1
                    };
                    var chamados = (IEnumerable<OrdemServico>)_osService.ObterPorParametros(parametros).Items;
                    await _integracaoZaffariService.ExecutarAsync(chamados);
                    task.DataHoraProcessamento = DateTime.Now;
                    task.Status = SatTaskStatusConst.PROCESSADO;
                    _taskService.Atualizar(task);

                    continue;
                }

                if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_MRP)
                {
                    _integracaoMRPService.ImportarArquivoMRPLogix();
                    _integracaoMRPService.ImportarArquivoMRPEstoqueLogix();
                    task.DataHoraProcessamento = DateTime.Now;
                    task.Status = SatTaskStatusConst.PROCESSADO;
                    _taskService.Atualizar(task);

                    continue;
                }

                if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO)
                {
                    _equipamentoContratoService.AtualizarParqueModelo();
                    task.DataHoraProcessamento = DateTime.Now;
                    task.Status = SatTaskStatusConst.PROCESSADO;
                    _taskService.Atualizar(task);

                    continue;
                }
            }
        }
    }
}