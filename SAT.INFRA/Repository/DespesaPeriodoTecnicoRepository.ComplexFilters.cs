using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public partial class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        public IQueryable<DespesaPeriodoTecnico> AplicarFiltroPeriodosAprovados(DespesaPeriodoTecnicoParameters parameters)
        {
            var despesasPeriodoTecnico = _context.DespesaPeriodoTecnico
                .Include(t => t.Tecnico)
                .Include(t => t.DespesaPeriodo)
                .AsQueryable();

            var despesaProtocoloPeriodoTecnico = _context.DespesaProtocoloPeriodoTecnico.AsQueryable();
            var aprovado = (int)DespesaPeriodoTecnicoStatusEnum.APROVADO;

            despesasPeriodoTecnico = from dpt in despesasPeriodoTecnico
                                     where dpt.CodDespesaPeriodoTecnicoStatus == aprovado
                                        && !(from dpp in despesaProtocoloPeriodoTecnico
                                             select dpp.CodDespesaPeriodoTecnico)
                                             .Contains(dpt.CodDespesaPeriodoTecnico)
                                     orderby dpt.Tecnico.Nome, dpt.DespesaPeriodo.DataInicio
                                     select dpt;

            return despesasPeriodoTecnico;
        }
    }
}