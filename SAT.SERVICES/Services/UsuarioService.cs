using Microsoft.Extensions.Configuration;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using SAT.MODELS.Helpers;

namespace SAT.SERVICES.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IConfiguration config,
            ITokenService tokenService
        )
        {
            _usuarioRepo = usuarioRepository;
            _config = config;
            _tokenService = tokenService;
        }

        public UsuarioLoginViewModel Login(Usuario usuario)
        {
            var usuarioLogado = _usuarioRepo.Login(usuario: usuario);

            for (int i = 0; i < usuarioLogado.Perfil?.NavegacoesConfiguracao?.Count; i++)
            {
                usuarioLogado.Perfil.NavegacoesConfiguracao.ToArray()[i].Navegacao.Id = usuarioLogado
                    .Perfil.NavegacoesConfiguracao.ToArray()[i].Navegacao.Title.ToLower();
            }

            var navegacoes = usuarioLogado.Perfil?.NavegacoesConfiguracao
                .Select(n => n.Navegacao).Where(n => n.CodNavegacaoPai == null && n.IndAtivo == 1).OrderBy(n => n.Ordem).ToList();

            if (navegacoes.Count == 0 && usuarioLogado.CodPerfil != 3)
                throw new Exception("Você não possui configurações de navegação, favor entrar em contato com a Equipe SAT");

            if (usuarioLogado.Perfil != null) usuarioLogado.Perfil.NavegacoesConfiguracao = null;
            var token = _tokenService.GerarToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), usuarioLogado);

            usuarioLogado.UltimoAcesso = DateTime.Now;
            _usuarioRepo.Atualizar(usuarioLogado);

            return new UsuarioLoginViewModel()
            {
                Usuario = usuarioLogado,
                Navegacoes = navegacoes,
                Token = token
            };
        }

        public Usuario ObterPorCodigo(string codigo)
        {
            return _usuarioRepo.ObterPorCodigo(codigo);
        }

        public ListViewModel ObterPorParametros(UsuarioParameters parameters)
        {
            var usuarios = _usuarioRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = usuarios,
                TotalCount = usuarios.TotalCount,
                CurrentPage = usuarios.CurrentPage,
                PageSize = usuarios.PageSize,
                TotalPages = usuarios.TotalPages,
                HasNext = usuarios.HasNext,
                HasPrevious = usuarios.HasPrevious
            };

            return lista;
        }

        public void Atualizar(Usuario usuario)
        {
            this._usuarioRepo.Atualizar(usuario);
        }

        public void AlterarSenha(SegurancaUsuarioModel segurancaUsuarioModel, bool forcaTrocarSenha = false)
        {
            this._usuarioRepo.AlterarSenha(segurancaUsuarioModel, forcaTrocarSenha);
        }

        public RecuperaSenha CriarRecuperaSenha(RecuperaSenha recuperaSenha)
        {
            return this._usuarioRepo.CriarRecuperaSenha(recuperaSenha);
        }

        public void AtualizarRecuperaSenha(RecuperaSenha recuperaSenha)
        {
            this._usuarioRepo.AtualizarRecuperaSenha(recuperaSenha);
        }

        public RecuperaSenha ObterRecuperaSenha(int codRecuperaSenha)
        {
            return this._usuarioRepo.ObterRecuperaSenha(codRecuperaSenha);
        }

        /// <summary>
        /// Faz a solicitação de nova senha
        /// </summary>
        /// <param name="codUsuario"></param>
        /// <returns></returns>
        public ResponseObject EsqueceuSenha(string codUsuario)
        {
            ResponseObject response = new();

            try
            {
                Usuario usuario = this._usuarioRepo.ObterPorCodigo(codUsuario);

                if (usuario == null)
                {
                    response.RequestValido = false;
                    response.Mensagem = "Código do usuário não encontrado";
                }
                else
                {
                    RecuperaSenha model = new() { CodUsuario = usuario.CodUsuario, Email = usuario.Email, DataHoraCad = DateTime.Now };

                    this.AtualizarRecuperaSenha(model);

                    model.IndAtivo = 1;

                    response.RequestValido = true;
                    response.Data = new Dictionary<string, object>
                    {
                        ["email"] = usuario.Email,
                        ["nomeUsuario"] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(usuario.NomeUsuario.ToLower()),
                        ["codRecuperaSenha"] = CriptoHelper.EnryptString(this.CriarRecuperaSenha(model).CodRecuperaSenha.ToString())
                    };
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Um erro ocorreu ao solicitar nova senha: {ex.Message} {ex.StackTrace} {ex.InnerException} {ex.Source}");
                response.RequestValido = false;
                response.Mensagem = "Um erro ocorreu ao solicitar nova senha";
            }

            return response;
        }

        /// <summary>
        /// Gera a nova senha
        /// </summary>
        /// <param name="codRecuperaSenhaCripto"></param>
        /// <returns></returns>
        public ResponseObject ConfirmaNovaSenha(string codRecuperaSenhaCripto)
        {
            ResponseObject response = new()
            {
                RequestValido = false
            };

            try
            {
                if (int.TryParse(CriptoHelper.DecryptString(codRecuperaSenhaCripto), out int codRecuperaSenha))
                {
                    RecuperaSenha recSenha = this.ObterRecuperaSenha(codRecuperaSenha);

                    if (recSenha != null)
                    {
                        if (!Convert.ToBoolean(recSenha.SolicitacaoConfirmada))
                        {
                            string novaSenha = PasswordHelper.GerarNovaSenha();

                            this.AlterarSenha(new SegurancaUsuarioModel() { CodUsuario = recSenha.CodUsuario, NovaSenha = novaSenha }, forcaTrocarSenha: true);

                            recSenha.SolicitacaoConfirmada = 1;
                            recSenha.IndAtivo = 0;

                            this.AtualizarRecuperaSenha(recSenha);

                            response.RequestValido = true;

                            Usuario usuario = this.ObterPorCodigo(recSenha.CodUsuario);

                            response.Data = new Dictionary<string, object>
                            {
                                ["email"] = usuario.Email,
                                ["nomeUsuario"] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(usuario.NomeUsuario.ToLower()),
                                ["senha"] = novaSenha
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerService.LogError($"Um erro ocorreu ao solicitar nova senha: {ex.Message} {ex.StackTrace} {ex.InnerException} {ex.Source}");
                response.RequestValido = false;
                response.Mensagem = "Um erro ocorreu ao solicitar nova senha";
            }

            return response;
        }
    }
}
