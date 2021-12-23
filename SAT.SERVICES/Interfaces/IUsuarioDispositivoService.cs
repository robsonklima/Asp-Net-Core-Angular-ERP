using SAT.MODELS.Entities;

namespace SAT.SERVICES.Interfaces
{
    public interface IUsuarioDispositivoService
    {
        void Criar(UsuarioDispositivo usuarioDispositivo);
        UsuarioDispositivo ObterPorUsuarioEHash(string codUsuario, string hash);
        void Atualizar(UsuarioDispositivo usuarioDispositivo);
    }
}
