using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;

namespace SAT.INFRA.Interfaces
{
    public interface IUsuarioRepository
    {
        Usuario Login(Usuario usuario);
        PagedList<Usuario> ObterPorParametros(UsuarioParameters parameters);
        Usuario ObterPorCodigo(string codigo);
        Usuario ObterLoginController(string codigo);
        void Atualizar(Usuario usuario);
        void Criar(Usuario usuario);
        void AlterarSenha(SegurancaUsuarioModel segurancaUsuarioModel, bool forcaTrocarSenha = false);
        RecuperaSenha CriarRecuperaSenha(RecuperaSenha recuperaSenha);
        RecuperaSenha ObterRecuperaSenha(int codRecuperaSenha);
        void AtualizarRecuperaSenha(RecuperaSenha recuperaSenha);
        void DesbloquearAcesso(string codUsuario);
    }
}
