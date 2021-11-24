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
        public IQueryable<DespesaPeriodoTecnico> AplicarFiltroPeriodosAprovados(IQueryable<DespesaPeriodoTecnico> query, DespesaPeriodoTecnicoParameters parameters)
        {
            var despesaProtocoloPeriodoTecnico = _context.DespesaProtocoloPeriodoTecnico
                .AsQueryable();

            var aprovado = (int)DespesaPeriodoTecnicoStatusEnum.APROVADO;

            query = from dpt in query
                    where dpt.CodDespesaPeriodoTecnicoStatus == aprovado
                       && !(from dpp in despesaProtocoloPeriodoTecnico
                            select dpp.CodDespesaPeriodoTecnico).ToString()
                            .Contains(dpt.CodDespesaPeriodoTecnico.ToString())
                    orderby dpt.Tecnico.Nome, dpt.DespesaPeriodo.DataInicio
                    select dpt;

            return query;
        }

        public IQueryable<DespesaPeriodoTecnico> AplicarFiltroCreditosCartao(IQueryable<DespesaPeriodoTecnico> query, DespesaPeriodoTecnicoParameters parameters)
        {
            query = AplicarFiltroPadrao(query, parameters);

            return query.Where(i =>
                i.CodDespesaPeriodoTecnicoStatus == (int)DespesaPeriodoTecnicoStatusEnum.APROVADO
                && i.Tecnico.DespesaCartaoCombustivelTecnico.Any() && i.DespesaProtocoloPeriodoTecnico != null);
        }
    }
}