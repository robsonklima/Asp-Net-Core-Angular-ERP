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
                var prazo = _ansService.CalcularPrazo(chamado);

                var pa = new OSPrazoAtendimento {
                    DataHoraCad = DateTime.Now,
                    DataHoraLimiteAtendimento = prazo,
                    CodOS = chamado.CodOS
                };

                _logger.Info($"{ MsgConst.SLA_CALCULADO } { chamado.CodOS }: { prazo }");
            }
        }
    }
}