using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoLoteService
    {
        ListViewModel ObterPorParametros(InstalacaoLoteParameters parameters);
        InstalacaoLote Criar(InstalacaoLote instalLote);
        void Deletar(int codigo);
        void Atualizar(InstalacaoLote instalLote);
        InstalacaoLote ObterPorCodigo(int codigo);
    }
}
