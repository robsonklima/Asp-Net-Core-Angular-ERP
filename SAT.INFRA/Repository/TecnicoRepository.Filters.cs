using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class TecnicoRepository : ITecnicoRepository
    {
        public IQueryable<Tecnico> AplicarFiltros(IQueryable<Tecnico> query, TecnicoParameters parameters)
        {
            switch (parameters.FilterType)
            {
                case TecnicoFilterEnum.FILTER_TECNICO_OS:
                    return PeriodoAtendimentoFilter(query, parameters);
                default:
                    return AplicarFiltroPadrao(query, parameters);
            }
        }
    }
}
