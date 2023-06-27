using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IntegrarModelos(SatTask task)
        {
            _equipamentoContratoService.AtualizarParqueModelo();
            
            _taskService.Atualizar(task);
        }
    }
}