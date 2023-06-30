using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Entities.Params;

namespace SAT.TASKS
{
    public partial class Worker : BackgroundService
    {
        private void IntegrarModelos(SatTask task)
        {
            try
            {
                var contrParams = new ContratoEquipamentoParameters { };
                var contratos = _contratoEquipamentoService.ObterPorParametros(contrParams).Items;

                _logger.Info($"{ MsgConst.INI_PROC }, encontrados { contratos.Count() } contratos");

                foreach (ContratoEquipamento contrato in contratos)
                {
                    var equipParams = new EquipamentoContratoParameters
                    {
                        CodEquips = contrato.CodEquip.ToString(),
                        CodContratos = contrato.CodContrato.ToString(),
                        IndAtivo = Constants.ATIVO
                    };

                    contrato.QtdEquipamentos = _equipamentoContratoService
                        .ObterPorParametros(equipParams)
                        .Items
                        .Count();
                    
                    _contratoEquipamentoService.Atualizar(contrato);
                    _logger.Info($"Atualizado contrato { contrato.CodContrato }, { contrato.QtdEquipamentos } equipamentos");
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