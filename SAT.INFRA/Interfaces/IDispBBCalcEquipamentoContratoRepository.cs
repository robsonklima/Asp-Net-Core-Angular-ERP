using SAT.MODELS.Entities;
using System.Collections.Generic;

namespace SAT.INFRA.Interfaces
{
    public interface IDispBBCalcEquipamentoContratoRepository
    {
        List<DispBBCalcEquipamentoContrato> ObterPorParametros(DispBBCalcEquipamentoContratoParameters parameters);
    }
}