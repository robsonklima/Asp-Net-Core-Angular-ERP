using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void ExecutarMRP(SatTask task)
        {
            _integracaoMRPService.ImportarArquivoMRPLogix();
            
            _integracaoMRPService.ImportarArquivoMRPEstoqueLogix();
        }
    }
}