using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;
using System;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarFiltroAgendaTecnico(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            query = query.Where(os => os.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO ||
                ((os.CodStatusServico == (int)StatusServicoEnum.ABERTO || os.CodStatusServico == (int)StatusServicoEnum.FECHADO) && os.DataHoraTransf.Value.Date == DateTime.Now.Date));

            return query;
        }
    }
}
