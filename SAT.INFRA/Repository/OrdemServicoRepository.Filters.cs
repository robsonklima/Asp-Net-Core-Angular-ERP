using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarFiltros(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            switch (parameters.FilterType)
            {
                case (OrdemServicoFilterEnum.FILTER_AGENDA):
                    query = AplicarFiltroAgendaTecnico(query, parameters);
                    break;
                default:
                    query = AplicarFiltroPadrao(query, parameters);
                    break;
            }
            return query;
        }
    }
}
