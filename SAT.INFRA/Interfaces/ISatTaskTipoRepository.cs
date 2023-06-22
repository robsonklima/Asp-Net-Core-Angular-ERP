using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ISatTaskTipoRepository
    {
        PagedList<SatTaskTipo> ObterPorParametros(SatTaskTipoParameters parameters);
        void Criar(SatTaskTipo task);
        void Deletar(int codigo);
        void Atualizar(SatTaskTipo task);
        SatTaskTipo ObterPorCodigo(int codigo);
    }
}
