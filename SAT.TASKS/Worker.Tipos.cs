using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private List<SatTaskTipo> ObterTipos()
        {
            _logger.Info(MsgConst.OBTENDO_TIPOS);

            List<SatTaskTipo> tipos = (List<SatTaskTipo>)_taskTipoService
                .ObterPorParametros(new SatTaskTipoParameters
                {
                    IndAtivo = (byte)Constants.ATIVO
                })
                .Items;

            _logger.Info($"{ MsgConst.TIPOS_OBTIDOS } { tipos.Count()} ");

            return tipos.ToList();
        }
    }
}