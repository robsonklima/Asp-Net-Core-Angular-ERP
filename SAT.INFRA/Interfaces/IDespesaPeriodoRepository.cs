using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaPeriodoRepository
    {
        PagedList<DespesaPeriodo> ObterPorParametros(DespesaPeriodoParameters parameters);
        IQueryable<DespesaPeriodo> ObterQuery(DespesaPeriodoParameters parameters);
        void Criar(DespesaPeriodo despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaPeriodo despesa);
        DespesaPeriodo ObterPorCodigo(int codigo);
    }
}