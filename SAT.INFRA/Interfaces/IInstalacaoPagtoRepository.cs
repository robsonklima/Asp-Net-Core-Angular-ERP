using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoPagtoRepository
    {
        void Criar(InstalacaoPagto instalacaoPagto);
        PagedList<InstalacaoPagto> ObterPorParametros(InstalacaoPagtoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(InstalacaoPagto instalacaoPagto);
        InstalacaoPagto ObterPorCodigo(int codigo);
    }
}
