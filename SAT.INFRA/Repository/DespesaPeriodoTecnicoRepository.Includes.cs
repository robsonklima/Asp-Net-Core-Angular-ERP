using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        public IQueryable<DespesaPeriodoTecnico> AplicarIncludes(IQueryable<DespesaPeriodoTecnico> query)
        {
            query = query
                .Include(dpt => dpt.DespesaPeriodo)
                .Include(dpt => dpt.Despesas)
                    .ThenInclude(dp => dp.DespesaItens)
                .Include(dpt => dpt.DespesaPeriodoTecnicoStatus)
                .Include(dpt => dpt.Tecnico)
                    .ThenInclude(dpt => dpt.Filial)
                .Include(dpt => dpt.Tecnico)
                    .ThenInclude(dpt => dpt.DespesaCartaoCombustivelTecnico)
                        .ThenInclude(dpt => dpt.DespesaCartaoCombustivel)
                            .ThenInclude(dpt => dpt.TicketLogUsuarioCartaoPlaca)
                .Include(dpt => dpt.DespesaProtocoloPeriodoTecnico)
                .Include(dpt => dpt.TicketLogPedidoCredito)
                .AsQueryable();

            return query;
        }
    }
}