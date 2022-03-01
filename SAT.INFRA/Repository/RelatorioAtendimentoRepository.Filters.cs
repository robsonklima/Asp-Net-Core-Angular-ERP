using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class RelatorioAtendimentoRepository : IRelatorioAtendimentoRepository
    {
        public IQueryable<RelatorioAtendimento> AplicarFiltros(IQueryable<RelatorioAtendimento> query, RelatorioAtendimentoParameters parameters)
        {
            return AplicarFiltroPadrao(query, parameters);
        }
    }
}