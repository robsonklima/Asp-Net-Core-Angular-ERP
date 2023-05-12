using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IMotivoComunicacaoService
    {
        ListViewModel ObterPorParametros(MotivoComunicacaoParameters parameters);
        MotivoComunicacao Criar(MotivoComunicacao mc);
        MotivoComunicacao Deletar(int codigo);
        MotivoComunicacao Atualizar(MotivoComunicacao mc);
        MotivoComunicacao ObterPorCodigo(int codigo);
    }
}
