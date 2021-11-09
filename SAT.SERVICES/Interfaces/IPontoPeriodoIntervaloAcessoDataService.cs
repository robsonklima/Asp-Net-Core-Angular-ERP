using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPontoPeriodoIntervaloAcessoDataService
    {
        ListViewModel ObterPorParametros(PontoPeriodoIntervaloAcessoDataParameters parameters);
        PontoPeriodoIntervaloAcessoData Criar(PontoPeriodoIntervaloAcessoData pontoPeriodoIntervaloAcessoData);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodoIntervaloAcessoData pontoPeriodoIntervaloAcessoData);
        PontoPeriodoIntervaloAcessoData ObterPorCodigo(int codigo);
    }
}
