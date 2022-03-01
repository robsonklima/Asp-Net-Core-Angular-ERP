using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IDispBBCalcEquipamentoContratoRepository
    {
        List<DispBBCalcEquipamentoContrato> ObterPorParametros(DispBBCalcEquipamentoContratoParameters parameters);
    }
}