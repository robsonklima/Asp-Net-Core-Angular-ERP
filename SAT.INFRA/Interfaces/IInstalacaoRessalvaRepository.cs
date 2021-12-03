using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoRessalvaRepository
    {
        void Criar(InstalacaoRessalva instalLote);
        PagedList<InstalacaoRessalva> ObterPorParametros(InstalacaoRessalvaParameters parameters);
        void Deletar(int codigo);
        void Atualizar(InstalacaoRessalva instalLote);
        InstalacaoRessalva ObterPorCodigo(int codigo);
    }
}
