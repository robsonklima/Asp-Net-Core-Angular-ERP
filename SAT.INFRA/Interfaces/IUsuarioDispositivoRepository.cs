using SAT.MODELS.Entities;

namespace SAT.INFRA.Interfaces
{
    public interface IUsuarioDispositivoRepository
    {
        void Criar(UsuarioDispositivo usuarioDispositivo);
        UsuarioDispositivo ObterPorUsuarioEHash(string codUsuario, string hash);
        void Atualizar(UsuarioDispositivo usuarioDispositivo);
    }
}
