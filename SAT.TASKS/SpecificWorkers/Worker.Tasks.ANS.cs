using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IntegrarANS(SatTask task, IEnumerable<OrdemServico> chamados)
        {
            foreach (var chamado in chamados)
            {
                var prazo = _ansService.CalcularSLA(chamado);

                _logger.Info($"{ MsgConst.SLA_CALCULADO } { chamado.CodOS }, resultado: { prazo }");
            }

            _taskService.Atualizar(task);
        }
    }
}