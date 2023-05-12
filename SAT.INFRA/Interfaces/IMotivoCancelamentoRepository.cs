using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IMotivoCancelamentoRepository
    {
        PagedList<MotivoCancelamento> ObterPorParametros(MotivoCancelamentoParameters parameters);
        MotivoCancelamento Criar(MotivoCancelamento m);
        MotivoCancelamento Deletar(int codigo);
        MotivoCancelamento Atualizar(MotivoCancelamento m);
        MotivoCancelamento ObterPorCodigo(int codigo);
    }
}
