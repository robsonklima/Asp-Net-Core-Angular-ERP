using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface ILocalEnvioNFFaturamentoVinculadoRepository
    {
        void Criar(LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado);
        PagedList<LocalEnvioNFFaturamentoVinculado> ObterPorParametros(LocalEnvioNFFaturamentoVinculadoParameters parameters);
        void Deletar(int codigo);
        void Atualizar(LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado);
        LocalEnvioNFFaturamentoVinculado ObterPorCodigo(int codLocalEnvioNFFaturamento, int codPosto, int codContrato);
    }
}
