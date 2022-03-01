using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITipoContratoService
    {
        ListViewModel ObterPorParametros(TipoContratoParameters parameters);
        TipoContrato Criar(TipoContrato tipoCausa);
        void Deletar(int codigo);
        void Atualizar(TipoContrato tipoCausa);
        TipoContrato ObterPorCodigo(int codigo);
    }
}
