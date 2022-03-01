using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface ILocalEnvioNFFaturamentoService
    {
        ListViewModel ObterPorParametros(LocalEnvioNFFaturamentoParameters parameters);
        LocalEnvioNFFaturamento Criar(LocalEnvioNFFaturamento localEnvioNFFaturamento);
        void Deletar(int codigo);
        void Atualizar(LocalEnvioNFFaturamento localEnvioNFFaturamento);
        LocalEnvioNFFaturamento ObterPorCodigo(int codigo);
    }
}
