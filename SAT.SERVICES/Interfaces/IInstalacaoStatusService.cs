using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoStatusService
    {
        ListViewModel ObterPorParametros(InstalacaoStatusParameters parameters);
        InstalacaoStatus Criar(InstalacaoStatus instalLote);
        void Deletar(int codigo);
        void Atualizar(InstalacaoStatus instalLote);
        InstalacaoStatus ObterPorCodigo(int codigo);
    }
}
