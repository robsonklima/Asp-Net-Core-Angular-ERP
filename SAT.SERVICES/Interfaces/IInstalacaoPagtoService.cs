using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoPagtoService
    {
        ListViewModel ObterPorParametros(InstalacaoPagtoParameters parameters);
        InstalacaoPagto Criar(InstalacaoPagto instalacaoPagto);
        void Deletar(int codigo);
        void Atualizar(InstalacaoPagto instalacaoPagto);
        InstalacaoPagto ObterPorCodigo(int codigo);
    }
}