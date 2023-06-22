using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface ISatTaskTipoService
    {
        ListViewModel ObterPorParametros(SatTaskTipoParameters parameters);
        SatTaskTipo Criar(SatTaskTipo SatTaskTipo);
        void Deletar(int codigo);
        void Atualizar(SatTaskTipo SatTaskTipo);
        SatTaskTipo ObterPorCodigo(int codigo);
    }
}
