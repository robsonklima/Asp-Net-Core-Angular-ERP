using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using System.Threading.Tasks;

namespace SAT.SERVICES.Interfaces
{
    public interface IUsuarioService
    {
        UsuarioLoginViewModel Login(Usuario usuario);
        ListViewModel ObterPorParametros(UsuarioParameters parameters);
        Usuario ObterPorCodigo(string codigo);
        void Atualizar(Usuario usuario);
        void AlterarSenha(SegurancaUsuarioModel segurancaUsuarioModel, bool forcaTrocarSenha = false);
        ResponseObject EsqueceuSenha(string email);
        ResponseObject ConfirmaNovaSenha(string codRecuperaSenhaCripto);
        RecuperaSenha CriarRecuperaSenha(RecuperaSenha recuperaSenha);
        RecuperaSenha ObterRecuperaSenha(int codRecuperaSenha);
        void AtualizarRecuperaSenha(RecuperaSenha recuperaSenha);


    }
}