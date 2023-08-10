using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void ExecutarEquipamentoNulo(SatTask task)
        {
            try
            {
                var osParams = new OrdemServicoParameters { 
                    CodEquipIsNull = true,
                    CodTipoIntervencaoNotIn = "10,14",
                    CodEquipContratoIsNotNull = true,
                    Include = OrdemServicoIncludeEnum.OS_INTEGRACAO
                };
                
                var chamados = _osService.ObterPorParametros(osParams).Items;

                _logger.Info($"{ MsgConst.INI_PROC }, encontrados { chamados.Count() } chamados");

                foreach (OrdemServico chamado in chamados)
                {
                    
                    if (chamado.EquipamentoContrato is not null)
                    {
                        chamado.CodEquip = _equipamentoContratoService.ObterPorCodigo(chamado.EquipamentoContrato.CodEquipContrato).CodEquip.Value;
                        
                        _osService.Atualizar(chamado);
                        _logger.Info($"{ MsgConst.INI_PROC }, Atualizado chamado { chamado.CodOS }");
                    }
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