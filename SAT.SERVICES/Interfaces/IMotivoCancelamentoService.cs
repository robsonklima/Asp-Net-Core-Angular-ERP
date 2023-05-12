using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IMotivoCancelamentoService
    {
        ListViewModel ObterPorParametros(MotivoCancelamentoParameters parameters);
        MotivoCancelamento Criar(MotivoCancelamento mc);
        MotivoCancelamento Deletar(int codigo);
        MotivoCancelamento Atualizar(MotivoCancelamento mc);
        MotivoCancelamento ObterPorCodigo(int codigo);
    }
}
