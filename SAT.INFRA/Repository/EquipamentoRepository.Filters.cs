using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class EquipamentoRepository : IEquipamentoRepository
    {
        public IQueryable<Equipamento> AplicarFiltros(IQueryable<Equipamento> query, EquipamentoParameters parameters)
        {
            switch (parameters.FilterType)
            {
                case EquipamentoFilterEnum.FILTER_CHAMADOS:
                    query = AplicarFiltroChamados(query, parameters);
                    break;
                default:
                    query = AplicarFiltroPadrao(query, parameters);
                    break;
            }
            return query;
        }
    }
}
