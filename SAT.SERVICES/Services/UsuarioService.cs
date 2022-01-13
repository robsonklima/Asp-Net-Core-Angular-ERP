using Microsoft.Extensions.Configuration;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System;
using System.Linq;

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

        public void Atualizar(Usuario usuario)
        {
            this._usuarioRepo.Atualizar(usuario);
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

        public void AlterarSenha(SegurancaUsuarioModel segurancaUsuarioModel)
        {
            throw new NotImplementedException();
        }
    }
}
