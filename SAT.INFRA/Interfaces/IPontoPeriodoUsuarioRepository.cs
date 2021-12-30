using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPontoPeriodoUsuarioRepository
    {
        PagedList<PontoPeriodoUsuario> ObterPorParametros(PontoPeriodoUsuarioParameters parameters);
        void Criar(PontoPeriodoUsuario pontoPeriodoUsuario);
        void Deletar(int codigo);
        void Atualizar(PontoPeriodoUsuario pontoPeriodoUsuario);
        PontoPeriodoUsuario ObterPorCodigo(int codigo);
    }
}