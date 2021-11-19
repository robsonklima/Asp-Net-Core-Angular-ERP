using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaPeriodoTecnicoRepository
    {
        PagedList<DespesaPeriodoTecnico> ObterPorParametros(DespesaPeriodoTecnicoParameters parameters);
        void Criar(DespesaPeriodoTecnico despesa);
        void Deletar(int codigo);
        void Atualizar(DespesaPeriodoTecnico despesaTecnico);
        DespesaPeriodoTecnico ObterPorCodigo(int codigo);
    }
}