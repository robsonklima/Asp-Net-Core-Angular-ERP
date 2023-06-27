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

            try
            {
                foreach (var tipo in tipos)
                {
                    SatTask task = (SatTask)_taskService
                        .ObterPorParametros(new SatTaskParameters { CodSatTaskTipo = tipo.CodSatTaskTipo })
                        .Items?
                        .FirstOrDefault()!;

                    var deveCriar = deveCriarTask(task); // justo

                    if (deveCriar)
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

            try
            {
                foreach (var task in tasks)
                {
                    int tipo = task.CodSatTaskTipo;
                    task.DataHoraProcessamento = DateTime.Now;
                    task.Status = SatTaskStatusConst.PROCESSADO;

                    if (tipo == (int)SatTaskTipoEnum.INT_BB)
                    {
                        _integracaoBBService.Processar();
                        _taskService.Atualizar(task);

                        continue;
                    }

                    if (tipo == (int)SatTaskTipoEnum.INT_BANRISUL)
                    {
                        await _integracaoBanrisulService.ProcessarEmailsAsync();
                        _integracaoBanrisulService.ProcessarRetornos();
                        _taskService.Atualizar(task);

                        continue;
                    }

                    if (tipo == (int)SatTaskTipoEnum.INT_ZAFFARI)
                    {
                        var parametros = new OrdemServicoParameters
                        {
                            CodCliente = Constants.CLIENTE_ZAFFARI,
                            DataHoraManutInicio = DateTime.Now.AddMinutes(-5),
                            DataHoraManutFim = DateTime.Now,
                        };
                        var chamados = (IEnumerable<OrdemServico>)_osService.ObterPorParametros(parametros).Items;
                        await _integracaoZaffariService.ExecutarAsync(chamados);
                        _taskService.Atualizar(task);

                        continue;
                    }

                    if (tipo == (int)SatTaskTipoEnum.INT_MRP)
                    {
                        _integracaoMRPService.ImportarArquivoMRPLogix();
                        _integracaoMRPService.ImportarArquivoMRPEstoqueLogix();
                        _taskService.Atualizar(task);

                        continue;
                    }

                    if (tipo == (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO)
                    {
                        _equipamentoContratoService.AtualizarParqueModelo();
                        _taskService.Atualizar(task);

                        continue;
                    }

                    if (tipo == (int)SatTaskTipoEnum.SLA)
                    {
                        var parametros = new OrdemServicoParameters
                        {
                            CodCliente = Constants.CLIENTE_ZAFFARI,
                            DataHoraManutInicio = DateTime.Now.AddMinutes(-5),
                            DataHoraManutFim = DateTime.Now,
                            CodTiposIntervencao = TipoIntervencaoEnum.CORRETIVA.ToString()
                        };
                        var chamados = (IEnumerable<OrdemServico>)_osService
                            .ObterPorParametros(parametros)
                            .Items;

                        foreach (var chamado in chamados)
                        {
                            var prazo = _ansService.CalcularSLA(chamado);

                            _logger.Info($"{MsgConst.SLA_CALCULADO} {chamado.CodOS}, resultado: {prazo}");
                        }

                        _taskService.Atualizar(task);

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