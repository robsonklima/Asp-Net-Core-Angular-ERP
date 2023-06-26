using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface ISatTaskProcessoService
    {
        ListViewModel ObterPorParametros(SatTaskProcessoParameters parameters);
        SatTaskProcesso Criar(SatTaskProcesso processo);
        SatTaskProcesso Deletar(int codigo);
        SatTaskProcesso Atualizar(SatTaskProcesso processo);
        SatTaskProcesso ObterPorCodigo(int codigo);
    }
}
