using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoNFVendaRepository
    {
        InstalacaoNFVenda Criar(InstalacaoNFVenda instalacao);
        PagedList<InstalacaoNFVenda> ObterPorParametros(InstalacaoNFVendaParameters parameters);
        void Deletar(int codigo);
        void Atualizar(InstalacaoNFVenda instalacao);
        InstalacaoNFVenda ObterPorCodigo(int codigo);
    }
}
