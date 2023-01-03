using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.Views;

namespace SAT.INFRA.Interfaces {
    public interface ITipoChamadoSTNRepository {
        PagedList<TipoChamadoSTN> ObterPorParametros(TipoChamadoSTNParameters parameters);
        TipoChamadoSTN ObterPorCodigo(int CodTipoChamadoSTN);
        void Criar(TipoChamadoSTN tipoChamadoSTN);
        void Deletar(int codigoTipoChamadoSTN);
        void Atualizar(TipoChamadoSTN tipoChamadoSTN);
    }
}