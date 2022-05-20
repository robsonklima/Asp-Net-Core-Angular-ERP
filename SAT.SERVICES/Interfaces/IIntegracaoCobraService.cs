using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IIntegracaoCobraService
    {
        ListViewModel ObterPorParametros(IntegracaoCobraParameters parameters);
        IntegracaoCobra Criar(IntegracaoCobra integracaoCobra);
        void Deletar(int codigo);
        void Atualizar(IntegracaoCobra integracaoCobra);
        IntegracaoCobra ObterPorCodigo(int codigo);
    }
}