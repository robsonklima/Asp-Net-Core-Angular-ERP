using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;
using System.Collections.Generic;

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


        public class OrdemServicoComparer : IEqualityComparer<OrdemServico>
        {
            public bool Equals(OrdemServico x, OrdemServico y) =>
                x.CodOS.Equals(y.CodOS);

            public int GetHashCode(OrdemServico obj) =>
                obj.CodOS.GetHashCode();
        }
    }
}