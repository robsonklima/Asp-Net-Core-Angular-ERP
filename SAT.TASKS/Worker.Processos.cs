using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void AtualizarFilaProcessos()
        {
            List<SatTaskTipo> tipos = ObterTipos();
            var processosPendentes = ObterProcessosPendentes();

            foreach (var tipo in tipos)
            {
                if (tipo.IndProcesso == 0)
                {
                    _logger.Info($"{MsgConst.TASK_NAO_POSSUI_PROCESSOS} {tipo.Nome}");

                    continue;
                }

                _logger.Info($"{MsgConst.OBTENDO_PROCESSOS} {tipo.Nome}");

                var parametros = new OrdemServicoParameters
                {
                    DataHoraManutInicio = DateTime.Now.AddMinutes(-6),
                    DataHoraManutFim = DateTime.Now,
                    CodClientes = ObterClientesProcessos()
                };
                var chamados = _osService.ObterPorParametros(parametros).Items;

                _logger.Info($"{MsgConst.QTD_CHAMADOS_ENVIO} {chamados.Count()}");

                foreach (OrdemServico chamado in chamados)
                {
                    var processo = ObterProcessoPendenteDoChamado(chamado.CodOS);

                    if (processo is null)
                    {
                        var p = new SatTaskProcesso
                        {
                            CodSatTaskTipo = tipo.CodSatTaskTipo,
                            DataHoraCad = DateTime.Now,
                            CodOS = chamado.CodOS,
                            Status = SatTaskStatusConst.PENDENTE,
                        };

                        _taskProcessoService.Criar(p);
                    }
                }
            }
        }

        private IEnumerable<SatTaskProcesso> ObterProcessosPendentes()
        {
            _logger.Info(MsgConst.OBTENDO_PROCESSOS_PENDENTES);

            var processos = (IEnumerable<SatTaskProcesso>)_taskProcessoService
                .ObterPorParametros(new SatTaskProcessoParameters
                {
                    Status = SatTaskStatusConst.PENDENTE,
                    SortActive = "CodSatTaskProcesso",
                    SortDirection = "DESC"
                })
                .Items;

            _logger.Info($"{MsgConst.QTD_PROCESSOS} {processos.Count()}");

            return processos;
        }

        private string ObterClientesProcessos()
        {
            return $"{Constants.CLIENTE_ZAFFARI}";
        }

        private SatTaskProcesso ObterProcessoPendenteDoChamado(int codOS)
        {
            var processos = (List<SatTaskProcesso>)_taskProcessoService
                .ObterPorParametros(new SatTaskProcessoParameters
                {
                    CodOS = codOS,
                    SortDirection = "DESC",
                    SortActive = "CodSatTaskProcesso"
                }).Items;

            _logger.Info($"{MsgConst.OBTENDO_PROCESSOS_CHAMADO} {codOS}, qtd: {processos.Count()}");

            return processos.FirstOrDefault()!;
        }
    }
}