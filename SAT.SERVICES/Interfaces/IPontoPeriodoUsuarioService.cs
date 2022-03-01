using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPontoPeriodoUsuarioService
    {
        ListViewModel ObterPorParametros(PontoPeriodoUsuarioParameters parameters);
        PontoPeriodoUsuario Criar(PontoPeriodoUsuario pontoPeriodoUsuario);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodoUsuario pontoPeriodoUsuario);
        PontoPeriodoUsuario ObterPorCodigo(int codigo);
    }
}
