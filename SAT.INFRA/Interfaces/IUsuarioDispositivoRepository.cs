using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Interfaces
{
    public interface IUsuarioDispositivoRepository
    {
        UsuarioDispositivo ObterPorCodigo(int codigo);
        UsuarioDispositivo Criar(UsuarioDispositivo usuarioDispositivo);
        PagedList<UsuarioDispositivo> ObterPorParametros(UsuarioDispositivoParameters parameters);
        void Atualizar(UsuarioDispositivo usuarioDispositivo);
    }
}
