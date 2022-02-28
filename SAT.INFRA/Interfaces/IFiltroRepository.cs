using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IFiltroRepository
    {
        void Criar(FiltroUsuario filtroUsuario);
        void Deletar(int codigo);
        void Atualizar(FiltroUsuario filtroUsuario);
        FiltroUsuario ObterPorCodigo(int codigo);
    }
}
