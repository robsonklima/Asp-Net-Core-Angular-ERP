using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPontoPeriodoStatusService
    {
        ListViewModel ObterPorParametros(PontoPeriodoStatusParameters parameters);
        PontoPeriodoStatus Criar(PontoPeriodoStatus pontoPeriodoStatus);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodoStatus pontoPeriodoStatus);
        PontoPeriodoStatus ObterPorCodigo(int codigo);
    }
}
