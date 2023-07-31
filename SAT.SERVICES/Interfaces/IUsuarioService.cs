using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;

namespace SAT.SERVICES.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioLoginViewModel Login(Usuario usuario);
        ListViewModel ObterPorParametros(UsuarioParameters parameters);
        Usuario ObterPorCodigo(string codigo);
        Usuario ObterLoginController(string codigo);
        void Atualizar(Usuario usuario);
        void Criar(Usuario usuario);
        void AlterarSenha(SegurancaUsuarioModel segurancaUsuarioModel, bool forcaTrocarSenha = false);
        ResponseObject EsqueceuSenha(string email);
        RecuperaSenha CriarRecuperaSenha(RecuperaSenha recuperaSenha);
        RecuperaSenha ObterRecuperaSenha(int codRecuperaSenha);
        void AtualizarRecuperaSenha(RecuperaSenha recuperaSenha);
        void DesbloquearAcesso(string codUsuario);
        
    }
}