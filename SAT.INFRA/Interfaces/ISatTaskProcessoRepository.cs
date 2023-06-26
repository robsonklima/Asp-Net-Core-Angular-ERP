using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ISatTaskProcessoRepository
    {
        PagedList<SatTaskProcesso> ObterPorParametros(SatTaskProcessoParameters parameters);
        SatTaskProcesso Criar(SatTaskProcesso processo);
        SatTaskProcesso Deletar(int codigo);
        SatTaskProcesso Atualizar(SatTaskProcesso processo);
        SatTaskProcesso ObterPorCodigo(int codigo);
    }
}
