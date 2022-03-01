using System.Collections.Generic;
using System.Linq;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;

namespace SAT.INFRA.Repository
{
    public class DispBBCalcEquipamentoContratoRepository : IDispBBCalcEquipamentoContratoRepository
    {
        private readonly AppDbContext _context;

        public DispBBCalcEquipamentoContratoRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<DispBBCalcEquipamentoContrato> ObterPorParametros(DispBBCalcEquipamentoContratoParameters parameters)
        {
            var dispBBCalc = _context
                        .DispBBCalcEquipamentoContrato
                        .AsQueryable();

            if (parameters.CodDispBBRegiao.HasValue)
                dispBBCalc = dispBBCalc.Where(c => c.CodDispBBRegiao == parameters.CodDispBBRegiao);

            if (parameters.Criticidade.HasValue)
                dispBBCalc = dispBBCalc.Where(c => c.Criticidade == parameters.Criticidade);

            if (!string.IsNullOrWhiteSpace(parameters.AnoMes))
                dispBBCalc = dispBBCalc.Where(c => c.AnoMes == parameters.AnoMes);

            return dispBBCalc.ToList();
        }
    }
}