using System.Linq;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaPeriodoTecnicoRepository
    {
        PagedList<DespesaPeriodoTecnico> ObterPorParametros(DespesaPeriodoTecnicoParameters parameters);

        IQueryable<DespesaPeriodoTecnico> ObterQuery(DespesaPeriodoTecnicoParameters parameters);
        void Criar(DespesaPeriodoTecnico despesa);
        void Deletar(int codigo);
        DespesaPeriodoTecnico Atualizar(DespesaPeriodoTecnico despesaTecnico);
        DespesaPeriodoTecnico ObterPorCodigo(int codigo);
    }
}