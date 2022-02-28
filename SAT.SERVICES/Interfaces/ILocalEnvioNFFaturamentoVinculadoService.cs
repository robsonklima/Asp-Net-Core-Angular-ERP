using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface ILocalEnvioNFFaturamentoVinculadoService
    {
        ListViewModel ObterPorParametros(LocalEnvioNFFaturamentoVinculadoParameters parameters);
        LocalEnvioNFFaturamentoVinculado Criar(LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado);
        void Deletar(int codigo);
        void Atualizar(LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado);
        LocalEnvioNFFaturamentoVinculado ObterPorCodigo(int codigo);
    }
}
