using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IInstalacaoTipoParcelaRepository
    {
        void Criar(InstalacaoTipoParcela instalacaoTipoParcela);
        PagedList<InstalacaoTipoParcela> ObterPorParametros(InstalacaoTipoParcelaParameters parameters);
        void Deletar(int codigo);
        void Atualizar(InstalacaoTipoParcela instalacaoTipoParcela);
        InstalacaoTipoParcela ObterPorCodigo(int codigo);
    }
}
