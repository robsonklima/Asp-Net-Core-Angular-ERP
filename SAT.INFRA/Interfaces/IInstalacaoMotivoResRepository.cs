using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoMotivoResRepository
    {
        PagedList<InstalacaoMotivoRes> ObterPorParametros(InstalacaoMotivoResParameters parameters);
        void Criar(InstalacaoMotivoRes transportadora);
        void Atualizar(InstalacaoMotivoRes transportadora);
        void Deletar(int codInstalacaoMotivoRes);
        InstalacaoMotivoRes ObterPorCodigo(int codigo);
    }
}
