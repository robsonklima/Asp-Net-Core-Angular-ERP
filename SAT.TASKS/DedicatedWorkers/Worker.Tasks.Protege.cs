using SAT.MODELS.Entities;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private async void ExecutarProtegeAsync(SatTask task, IEnumerable<OrdemServico> chamados)
        {
            var token = await _integracaoProtegeService
                .LoginAsync();
            
            var osCliente = await _integracaoProtegeService
                .ConsultarChamadoAsync(token, "564955", "6dd53665c0c24cab86870a21cf6434ae");

            var armazenados = await _integracaoProtegeService
                .ConsultarPesquisaArmazenadaAsync(token);

            var osClienteEnviada = await _integracaoProtegeService
                .EnviarChamadoAsync(token);

            var osClienteAtualizada = await _integracaoProtegeService
                .AtualizarStatusChamadoAsync(token);
        }
    }
}