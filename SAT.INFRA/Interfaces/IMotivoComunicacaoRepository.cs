using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IMotivoComunicacaoRepository
    {
        PagedList<MotivoComunicacao> ObterPorParametros(MotivoComunicacaoParameters parameters);
        MotivoComunicacao Criar(MotivoComunicacao m);
        MotivoComunicacao Deletar(int codigo);
        MotivoComunicacao Atualizar(MotivoComunicacao m);
        MotivoComunicacao ObterPorCodigo(int codigo);
    }
}
