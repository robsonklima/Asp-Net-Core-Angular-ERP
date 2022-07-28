using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Interfaces
{
    public interface ILocalEnvioNFFaturamentoVinculadoService
    {
        ListViewModel ObterPorParametros(LocalEnvioNFFaturamentoVinculadoParameters parameters);
        LocalEnvioNFFaturamentoVinculado Criar(LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado);
        void Deletar(int codLocalEnvioNFFaturamento, int codPosto, int codContrato);
        void Atualizar(LocalEnvioNFFaturamentoVinculado localEnvioNFFaturamentoVinculado);
        LocalEnvioNFFaturamentoVinculado ObterPorCodigo(int codLocalEnvioNFFaturamento, int codPosto, int codContrato);
    }
}
