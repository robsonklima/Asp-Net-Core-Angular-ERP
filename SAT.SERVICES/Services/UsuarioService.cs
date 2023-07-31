using Microsoft.Extensions.Configuration;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;
using SAT.UTILS;
using NLog;
using NLog.Fluent;
using SAT.MODELS.Entities.Constants;

namespace SAT.SERVICES.Services
{
    public class UsuarioService : IUsuarioService
    {
        private static readonly Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IUsuarioLoginRepository _usuarioLoginRepo;
        private readonly IFotoService _fotoService;

        public UsuarioService(
            IUsuarioRepository usuarioRepository,
            IConfiguration config,
            ITokenService tokenService,
            IUsuarioLoginRepository usuarioLoginRepo,
            IFotoService fotoService
        )
        {
            _usuarioRepo = usuarioRepository;
            _config = config;
            _tokenService = tokenService;
            _usuarioLoginRepo = usuarioLoginRepo;
            _fotoService = fotoService;
        }

        public UsuarioLoginViewModel Login(Usuario usuario)
        {
            var usuarioLogado = _usuarioRepo.Login(usuario: usuario);
            usuarioLogado.Foto = _fotoService.BuscarFotoUsuario(usuarioLogado.CodUsuario);
            var navegacoes = CarregarNavegacoes(usuarioLogado);
            RegistrarAcesso(usuarioLogado);

            if (usuarioLogado.NavegacoesConfiguracao != null) usuarioLogado.NavegacoesConfiguracao = null;
            var token = _tokenService.GerarToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), usuarioLogado);

            return new UsuarioLoginViewModel()
            {
                Usuario = usuarioLogado,
                Navegacoes = navegacoes,
                Token = token
            };
        }

        public Usuario ObterPorCodigo(string codigo)
        {
            var usuario = _usuarioRepo.ObterPorCodigo(codigo);
            
            return usuario;
        }

        public Usuario ObterLoginController(string codigo)
        {
            var usuario = _usuarioRepo.ObterLoginController(codigo);
            
            return usuario;
        }

        public ListViewModel ObterPorParametros(UsuarioParameters parameters)
        {
            var usuarios = _usuarioRepo.ObterPorParametros(parameters);

            for (int i = 0; i < usuarios.Count; i++)
            {
                usuarios[i].Foto = _fotoService.BuscarFotoUsuario(usuarios[i].CodUsuario);
            }

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
                    _logger.Error()
                        .Message("Código do usuário não encontrado")
                        .Property("application", Constants.SISTEMA_NOME)
                        .Write();
                }
                else
                {
                    RecuperaSenha model = new() { CodUsuario = usuario.CodUsuario, Email = usuario.Email, DataHoraCad = DateTime.Now };

                    model.IndAtivo = 1;
                    model.SolicitacaoConfirmada = 1;

                    this.CriarRecuperaSenha(model);

                    string novaSenha = PasswordHelper.GerarNovaSenha();

                    this.AlterarSenha(new SegurancaUsuarioModel() { CodUsuario = usuario.CodUsuario, NovaSenha = novaSenha }, forcaTrocarSenha: true);

                    response.RequestValido = true;

                    response.Data = new Dictionary<string, object>
                    {
                        ["email"] = usuario.Email,
                        ["nomeUsuario"] = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(usuario.NomeUsuario.ToLower()),
                        ["senha"] = novaSenha
                    };
                }
            }
            catch (Exception ex)
            {
                _logger.Error()
                    .Message($"Um erro ocorreu ao solicitar nova senha: {ex.Message} {ex.StackTrace} {ex.InnerException} {ex.Source}")
                    .Property("application", "SAT 2.0 API")
                    .Write();
                response.RequestValido = false;
                response.Mensagem = "Um erro ocorreu ao solicitar nova senha";
            }

            return response;
        }

        public void Criar(Usuario usuario)
        {
            this._usuarioRepo.Criar(usuario);
        }

        private List<Navegacao> CarregarNavegacoes(Usuario usuario)
        {
            for (int i = 0; i < usuario.NavegacoesConfiguracao?.Count; i++)
            {
                usuario.NavegacoesConfiguracao.ToArray()[i].Navegacao.Id = usuario.NavegacoesConfiguracao.ToArray()[i].Navegacao.Title.ToLower();
            }

            var navegacoes = usuario.NavegacoesConfiguracao
               .Select(n => n.Navegacao)
               .Where(n => n.CodNavegacaoPai == null && n.IndAtivo == 1)
               .OrderBy(n => n.Ordem)
               .ThenBy(t => t.Title)
               .ToList();

            for (int i = 0; i < navegacoes?.Count; i++)
            {
                if (navegacoes[i].Children == null) continue;

                navegacoes[i].Children = navegacoes[i].Children
                    .OrderBy(ord => ord.Ordem)
                    .ThenBy(t => t.Title)
                    .ToArray();
            }

            if (navegacoes?.Count == 0)
            throw new Exception("Você não possui configurações de navegação, favor entrar em contato com a Equipe SAT");
            
            return navegacoes;
        }

        public void DesbloquearAcesso(string codUsuario)
        {
            this._usuarioRepo.DesbloquearAcesso(codUsuario);
        }

        private void RegistrarAcesso(Usuario usuarioLogado) 
        {
            try
            {
                usuarioLogado.UltimoAcesso = DateTime.Now;
                _usuarioRepo.Atualizar(usuarioLogado);

                var login = new UsuarioLogin {
                    CodUsuario = usuarioLogado?.CodUsuario,
                    DataHoraCad = DateTime.Now,
                    Servidor = System.Environment.MachineName
                };

                _usuarioLoginRepo.Criar(login);   
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao registrar acesso do usuário", ex);
            }
        }
    }
}
