using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoMotivoResService
    {
        ListViewModel ObterPorParametros(InstalacaoMotivoResParameters parameters);
        InstalacaoMotivoRes Criar(InstalacaoMotivoRes transportadora);
        void Deletar(int codigo);
        void Atualizar(InstalacaoMotivoRes transportadora);
        InstalacaoMotivoRes ObterPorCodigo(int codigo);
    }
}
