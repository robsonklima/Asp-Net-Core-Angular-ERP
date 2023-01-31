using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoAnexoRepository
    {
        void Criar(InstalacaoAnexo instalacaoAnexo);
        void Deletar(int codigo);
        InstalacaoAnexo ObterPorCodigo(int codigo);
        PagedList<InstalacaoAnexo> ObterPorParametros(InstalacaoAnexoParameters parameters);
    }
}