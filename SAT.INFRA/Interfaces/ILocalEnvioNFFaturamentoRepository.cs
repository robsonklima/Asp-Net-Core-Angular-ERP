using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ILocalEnvioNFFaturamentoRepository
    {
        void Criar(LocalEnvioNFFaturamento localEnvioNFFaturamento);
        PagedList<LocalEnvioNFFaturamento> ObterPorParametros(LocalEnvioNFFaturamentoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(LocalEnvioNFFaturamento localEnvioNFFaturamento);
        LocalEnvioNFFaturamento ObterPorCodigo(int codigo);
    }
}
