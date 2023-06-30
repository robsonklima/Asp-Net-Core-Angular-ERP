using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IntegrarMRP(SatTask task)
        {
            //_integracaoMRPService.ImportarArquivoMRPLogix();
            
            _integracaoMRPService.ImportarArquivoMRPEstoqueLogix();
        }
    }
}