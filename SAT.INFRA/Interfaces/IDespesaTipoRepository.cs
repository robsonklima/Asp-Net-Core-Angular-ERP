using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IDespesaTipoRepository
    {
        PagedList<DespesaTipo> ObterPorParametros(DespesaTipoParameters parameters);
        void Criar(DespesaTipo despesaTipo);
        void Deletar(int codigo);
        void Atualizar(DespesaTipo despesaTipo);
        DespesaTipo ObterPorCodigo(int codigo);
    }
}