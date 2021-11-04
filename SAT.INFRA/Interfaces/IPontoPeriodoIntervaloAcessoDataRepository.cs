using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPontoPeriodoIntervaloAcessoDataRepository
    {
        PagedList<PontoPeriodoIntervaloAcessoData> ObterPorParametros(PontoPeriodoIntervaloAcessoDataParameters parameters);
        void Criar(PontoPeriodoIntervaloAcessoData pontoPeriodoIntervaloAcessoData);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodoIntervaloAcessoData pontoPeriodoIntervaloAcessoData);
        PontoPeriodoIntervaloAcessoData ObterPorCodigo(int codigo);
    }
}