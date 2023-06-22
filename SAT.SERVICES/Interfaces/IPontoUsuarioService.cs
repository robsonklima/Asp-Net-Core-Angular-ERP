using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IPontoUsuarioService
    {
        ListViewModel ObterPorParametros(PontoUsuarioParameters parameters);
        PontoUsuario Criar(PontoUsuario pontoUsuario);
        void Deletar(int codigo);
        void Atualizar(PontoUsuario pontoUsuario);
        PontoUsuario ObterPorCodigo(int codigo);
    }
}
