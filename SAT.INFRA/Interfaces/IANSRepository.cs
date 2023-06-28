using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.Views;

namespace SAT.INFRA.Interfaces {
    public interface IANSRepository {
        PagedList<ANS> ObterPorParametros(ANSParameters parameters);
        ANS ObterPorCodigo(int codigoAns);
        ANS Criar(ANS ans);
        ANS Deletar(int codigoAns);
        ANS Atualizar(ANS ans);
    }
}