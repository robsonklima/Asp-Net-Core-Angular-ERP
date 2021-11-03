using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IPontoUsuarioRepository
    {
        PagedList<PontoUsuario> ObterPorParametros(PontoUsuarioParameters parameters);
        void Criar(PontoUsuario pontoUsuario);
        void Deletar(int codigo);
        void Atualizar(PontoUsuario pontoUsuario);
        PontoUsuario ObterPorCodigo(int codigo);
    }
}