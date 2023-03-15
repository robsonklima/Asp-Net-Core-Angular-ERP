using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoMotivoMultaRepository
    {
        void Criar(InstalacaoMotivoMulta instalacaoMotivoMulta);
        PagedList<InstalacaoMotivoMulta> ObterPorParametros(InstalacaoMotivoMultaParameters parameters);
        void Deletar(int codigo);
        void Atualizar(InstalacaoMotivoMulta instalacaoMotivoMulta);
        InstalacaoMotivoMulta ObterPorCodigo(int codigo);
    }
}
