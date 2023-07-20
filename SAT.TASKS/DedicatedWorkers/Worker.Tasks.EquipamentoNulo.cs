using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void ExecutarEquipamentoNulo(SatTask task)
        {
            try
            {
                var osParams = new OrdemServicoParameters {};
                osParams.CodEquipIsNull = true;
                var chamados = _osService.ObterPorParametros(osParams).Items;

                _logger.Info($"{ MsgConst.INI_PROC }, encontrados { contratos.Count() } contratos");

                foreach (OrdemServico chamado in chamados)
                {
                    chamado.CodEquip = _equipamentoContratoService.ObterPorCodigo(chamado.CodEquipContrato).CodEquip.Value;
                    
                    _osService.Atualizar(chamado);
                    _logger.Info($"Atualizado chamado { chamado.CodOS }");
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"{ ex.Message }");
            }
           
            _logger.Info($"{ MsgConst.FIN_PROC }");
        }
    }
}