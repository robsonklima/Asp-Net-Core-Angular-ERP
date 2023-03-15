using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface IInstalacaoTipoParcelaService
    {
        ListViewModel ObterPorParametros(InstalacaoTipoParcelaParameters parameters);
        InstalacaoTipoParcela Criar(InstalacaoTipoParcela instalacaoTipoParcela);
        void Deletar(int codigo);
        void Atualizar(InstalacaoTipoParcela instalacaoTipoParcela);
        InstalacaoTipoParcela ObterPorCodigo(int codigo);
    }
}