using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPontoPeriodoUsuarioStatusService
    {
        ListViewModel ObterPorParametros(PontoPeriodoUsuarioStatusParameters parameters);
        PontoPeriodoUsuarioStatus Criar(PontoPeriodoUsuarioStatus pontoPeriodoUsuarioStatus);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodoUsuarioStatus pontoPeriodoUsuarioStatus);
        PontoPeriodoUsuarioStatus ObterPorCodigo(int codigo);
    }
}
