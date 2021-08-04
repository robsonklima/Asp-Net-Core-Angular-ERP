using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.API.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public Usuario Login(Usuario usuario)
        {
            if (ValidateLogin(usuario))
            {
                return _context.Usuario
                        .Include(u => u.Filial)
                        .Include(u => u.Perfil)
                        .Include(u => u.Perfil.NavegacoesConfiguracao)
                            .ThenInclude(conf => conf.Navegacao)
                        .FirstOrDefault(u => u.CodUsuario == usuario.CodUsuario);
            }
            else
            {
                throw new Exception(Constants.USUARIO_OU_SENHA_INVALIDOS);
            }
        }

        private bool ValidateLogin(Usuario usuario)
        {
            var usuarios = _context.Usuario.FromSqlRaw(string.Format(@"
                SELECT	    *
                FROM	    Usuario
                WHERE	    CodUsuario = '{0}'
                AND		    (PWDCompare('{1}', Senha) = 1)", usuario.CodUsuario, usuario.Senha)).ToList();

            if (usuarios.Count > 0)
            {
                return true;
            }

            return false;
        }

        public PagedList<Usuario> ObterPorParametros(UsuarioParameters parameters)
        {
            var usuarios = _context.Usuario
                .Include(u => u.Perfil)
                .Include(u => u.Tecnico)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                usuarios = usuarios.Where(
                    u =>
                    u.NomeUsuario.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    u.CodUsuario.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodUsuario != null)
            {
                usuarios = usuarios.Where(u => u.CodUsuario == parameters.CodUsuario);
            }

            if (parameters.CodPerfil != null)
            {
                usuarios = usuarios.Where(u => u.CodPerfil == parameters.CodPerfil);
            }

            if (parameters.CodFilial != null)
            {
                usuarios = usuarios.Where(u => u.CodFilial == parameters.CodFilial);
            }

            if (parameters.IndAtivo != null)
            {
                usuarios = usuarios.Where(u => u.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                usuarios = usuarios.OrderBy(parameters.SortActive, parameters.SortDirection);
            }

            usuarios = usuarios.Include(u => u.Localizacoes.ToList().OrderByDescending(o => o.CodLocalizacao).Take(1));
            
            return PagedList<Usuario>.ToPagedList(usuarios, parameters.PageNumber, parameters.PageSize);
        }

        public Usuario ObterPorCodigo(string codigo)
        {
            return _context.Usuario.FirstOrDefault(us => us.CodUsuario == codigo);
        }
    }
}
