using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void ExecutarANS(SatTask task, IEnumerable<OrdemServico> chamados)
        {
            foreach (var chamado in chamados)
            {
                var prazo = _ansService.CalcularPrazo(chamado.CodOS);

                if (prazo is not null)
                {
                    _prazoAtendimentoService.Criar(new OSPrazoAtendimento
                    {
                        DataHoraCad = DateTime.Now,
                        DataHoraLimiteAtendimento = prazo,
                        CodOS = chamado.CodOS
                    });
                    
                    _logger.Info($"{MsgConst.SLA_CALCULADO} {chamado.CodOS}: {prazo}");
                }
                else
                    _logger.Error($"{MsgConst.SLA_NAO_CALCULADO} {chamado.CodOS}");
            }
        }
    }
}