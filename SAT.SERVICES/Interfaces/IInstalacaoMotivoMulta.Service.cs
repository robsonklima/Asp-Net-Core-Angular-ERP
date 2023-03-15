using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoMotivoMultaService
    {
        ListViewModel ObterPorParametros(InstalacaoMotivoMultaParameters parameters);
        InstalacaoMotivoMulta Criar(InstalacaoMotivoMulta instalacaoMotivoMulta);
        void Deletar(int codigo);
        void Atualizar(InstalacaoMotivoMulta instalacaoMotivoMulta);
        InstalacaoMotivoMulta ObterPorCodigo(int codigo);
    }
}