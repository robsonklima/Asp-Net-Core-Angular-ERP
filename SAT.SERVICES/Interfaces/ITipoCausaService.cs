using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITipoCausaService
    {
        ListViewModel ObterPorParametros(TipoCausaParameters parameters);
        TipoCausa Criar(TipoCausa tipoCausa);
        void Deletar(int codigo);
        void Atualizar(TipoCausa tipoCausa);
        TipoCausa ObterPorCodigo(int codigo);
    }
}
