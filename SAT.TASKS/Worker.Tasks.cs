using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void AtualizarFilaTasks(List<SatTaskTipo> tipos)
        {
            _logger.Info(MsgConst.INI_PROC_FILA);

            foreach (var tipo in tipos)
            {
                SatTask task = ObterTask(tipo);

                var criar = deveCriarTask(task);

                if (criar)
                {
                    _logger.Info(MsgConst.CRIANDO_TASK);

                    var novaTask = CriarTask(tipo.CodSatTaskTipo);

                    _logger.Info($"{MsgConst.TASK_CRIADA} {novaTask.CodSatTask}, {novaTask.Tipo.Nome}");
                }
                else
                {
                    _logger.Info($"{MsgConst.EXISTE_TASK_PENDENTE} {task.CodSatTask}, {task.Tipo.Nome}");
                }
            }

            _logger.Info(MsgConst.FIN_PROC_FILA);
        }

        private bool deveCriarTask(SatTask task)
        {
            if (task is null)
            {
                _logger.Info(MsgConst.NENHUM_REG_ENCONTRADO);

                return true;
            }

            if (task.Status == SatTaskStatusConst.PENDENTE)
            {
                _logger.Info(MsgConst.TASK_PENDENTE);

                return false;
            }

            return ObterPermissaoProcessamento(task);
        }

        private bool ObterPermissaoProcessamento(SatTask task)
        {
            switch (task.CodSatTaskTipo)
            {
                case (int)SatTaskTipoEnum.INT_BANRISUL:
                    _logger.Info($"{MsgConst.OBTENDO_PERMISSAO} {Constants.INTEGRACAO_BANRISUL_ATM}");
                    return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_BANRISUL_TEMPO_MIN);
                case (int)SatTaskTipoEnum.INT_BB:
                    _logger.Info($"{MsgConst.OBTENDO_PERMISSAO} {Constants.INTEGRACAO_BB}");
                    return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_BB_TEMPO_MIN);
                case (int)SatTaskTipoEnum.INT_ZAFFARI:
                    _logger.Info($"{MsgConst.OBTENDO_PERMISSAO} {Constants.INTEGRACAO_ZAFFARI}");
                    return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_ZAFFARI_TEMPO_MIN);
                case (int)SatTaskTipoEnum.INT_MRP:
                    _logger.Info($"{MsgConst.OBTENDO_PERMISSAO} {Constants.INTEGRACAO_LOGIX_MRP}");
                    return task.DataHoraProcessamento <= DateTime.Now.AddMinutes(-(int)Constants.INTEGRACAO_LOGIX_MRP_TEMPO_MIN);
                case (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO:
                    _logger.Info($"{MsgConst.OBTENDO_PERMISSAO} {Constants.ATUALIZACAO_PARQUE_MODELO}");
                    return task.DataHoraProcessamento <= DateTime.Now.AddDays(-(int)Constants.ATUALIZACAO_PARQUE_MODELO_TEMPO_MIN);
                default:
                    return false;
            }
        }

        private void AtualizarTask(SatTask task)
        {
            _logger.Info(MsgConst.ATUALIZANDO_TASK);

            task.DataHoraProcessamento = DateTime.Now;
            task.Status = SatTaskStatusConst.PROCESSADO;

            _taskService.Atualizar(task);

            _logger.Info($"{MsgConst.TASK_ATUALIZADA} {task.CodSatTask}");
        }

        private SatTask CriarTask(int tipo)
        {
            _logger.Info(MsgConst.CRIANDO_TASK);

            var task = _taskService.Criar(new SatTask
            {
                Status = SatTaskStatusConst.PENDENTE,
                DataHoraCad = DateTime.Now,
                CodSatTaskTipo = tipo
            });

            _logger.Info($"{MsgConst.TASK_CRIADA} {task.CodSatTask}");

            return task;
        }

        private IEnumerable<SatTask> ObterTasksPendentes()
        {
            _logger.Info(MsgConst.OBTENDO_TASKS_PENDENTES);

            var tasks = (IEnumerable<SatTask>)_taskService.ObterPorParametros(new SatTaskParameters
            {
                Status = SatTaskStatusConst.PENDENTE,
                SortActive = "CodSatTask",
                SortDirection = "DESC"
            })
            .Items;

            _logger.Info($"{MsgConst.TASKS_OBTIDAS} {tasks.Count()}");

            return tasks;
        }

        private SatTask ObterTask(SatTaskTipo tipo)
        {
            _logger.Info($"{MsgConst.OBTENDO_ULT_TASK} {tipo.Nome}");

            var task = (SatTask)_taskService
                    .ObterPorParametros(new SatTaskParameters
                    {
                        CodSatTaskTipo = tipo.CodSatTaskTipo,
                        SortActive = "CodSatTask",
                        SortDirection = "DESC"
                    })
                    .Items?
                    .FirstOrDefault()!;

            if (task is not null)
                _logger.Info($"{MsgConst.ULT_TASK_OBTIDA} {task.CodSatTask}");
            else
                _logger.Info(MsgConst.REG_NAO_ENCONTRADO);

            return task!;
        }

        private async Task ProcessarFilaTasksAsync(IEnumerable<SatTask> tasks)
        {
            _logger.Info(MsgConst.INI_PROC_TASKS);

            foreach (var task in tasks)
            {
                if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BB)
                {
                    _integracaoBBService.Processar();
                    AtualizarTask(task);

                    continue;
                }

                if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_BANRISUL)
                {
                    await _integracaoBanrisulService.ProcessarEmailsAsync();
                    _integracaoBanrisulService.ProcessarRetornos();
                    AtualizarTask(task);

                    continue;
                }

                if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_ZAFFARI)
                {
                    var parametros = new OrdemServicoParameters { 
                        CodCliente = Constants.CLIENTE_ZAFFARI,
                        DataHoraManutInicio = DateTime.Now.AddMinutes(-5),
                        DataHoraManutFim = DateTime.Now,
                        IndServico = 1,
                        IndIntegracao = 1
                    };
                    var chamados = (IEnumerable<OrdemServico>)_osService.ObterPorParametros(parametros).Items;
                    await _integracaoZaffariService.ExecutarAsync(chamados);
                    AtualizarTask(task);

                    continue;
                }

                if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.INT_MRP)
                {
                    _integracaoMRPService.ImportarArquivoMRPLogix();
                    _integracaoMRPService.ImportarArquivoMRPEstoqueLogix();
                    AtualizarTask(task);

                    continue;
                }

                if (task.CodSatTaskTipo == (int)SatTaskTipoEnum.ATUALIZACAO_PARQUE_MODELO)
                {
                    _equipamentoContratoService.AtualizarParqueModelo();
                    AtualizarTask(task);
                    continue;
                }
            }
        }
    }
}