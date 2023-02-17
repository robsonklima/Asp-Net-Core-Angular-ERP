using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Enums;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public partial class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        public IQueryable<DespesaPeriodoTecnico> AplicarFiltroPeriodosAprovados(IQueryable<DespesaPeriodoTecnico> query, DespesaPeriodoTecnicoParameters parameters)
        {
            var despesaProtocoloPeriodoTecnico = _context.DespesaProtocoloPeriodoTecnico.AsQueryable();


            query = from dpt in query
                    where dpt.CodDespesaPeriodoTecnicoStatus == (int)DespesaPeriodoTecnicoStatusEnum.REPROVADO
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

            query = query.Where(i =>
                i.CodDespesaPeriodoTecnicoStatus == (int)DespesaPeriodoTecnicoStatusEnum.APROVADO
                && i.Tecnico.DespesaCartaoCombustivelTecnico.Any() && i.DespesaProtocoloPeriodoTecnico != null &&
                i.DataHoraCad.Year >= 2022).OrderByDescending(i => i.DespesaProtocoloPeriodoTecnico.DataHoraCad);

            if (parameters.CodCreditoCartaoStatus.HasValue)
            {
                switch (parameters.CodCreditoCartaoStatus)
                {
                    case DespesaCreditoCartaoStatusEnum.COMPENSADO:
                        query = query.Where(i => i.IndCompensacao == 1);
                        break;
                    case DespesaCreditoCartaoStatusEnum.CREDITADO:
                        query = query.Where(i => i.IndCredito == 1 && string.IsNullOrEmpty(i.TicketLogPedidoCredito.Observacao));
                        break;
                    case DespesaCreditoCartaoStatusEnum.ERRO:
                        query = query.Where(i => i.IndCredito == 1 && !string.IsNullOrEmpty(i.TicketLogPedidoCredito.Observacao));
                        break;
                    case DespesaCreditoCartaoStatusEnum.PENDENTE:
                        query = query.Where(i => (i.IndCredito == 0 || !i.IndCredito.HasValue) && (i.IndCompensacao == 0 || !i.IndCompensacao.HasValue));
                        break;
                    case DespesaCreditoCartaoStatusEnum.VERIFICADO:
                        query = query.Where(i => i.IndVerificacao == 1);
                        break;
                    default:
                        break;
                }
            }

            return query;
        }
    }
}