using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IUsuarioDispositivoService
    {
        ListViewModel ObterPorParametros(UsuarioDispositivoParameters parameters);
        UsuarioDispositivo Criar(UsuarioDispositivo usuarioDispositivo);
        void Atualizar(UsuarioDispositivo usuarioDispositivo);
        UsuarioDispositivo ObterPorCodigo(int codigo);
    }
}
