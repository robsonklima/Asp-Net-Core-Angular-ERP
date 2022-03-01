using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ITipoIndiceReajusteService
    {
        ListViewModel ObterPorParametros(TipoIndiceReajusteParameters parameters);
        TipoIndiceReajuste Criar(TipoIndiceReajuste tipoIndiceReajuste);
        void Deletar(int codigo);
        void Atualizar(TipoIndiceReajuste tipoIndiceReajuste);
        TipoIndiceReajuste ObterPorCodigo(int codigo);
    }
}
