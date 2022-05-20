using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IIntegracaoCobraRepository
    {
        void Criar(IntegracaoCobra integracaoCobra);
        PagedList<IntegracaoCobra> ObterPorParametros(IntegracaoCobraParameters parameters);
        void Deletar(int codigo);
        void Atualizar(IntegracaoCobra integracaoCobra);
        IntegracaoCobra ObterPorCodigo(int CodOS, string NumOSCliente, string NomeTipoArquivoEnviado);
    }
}
