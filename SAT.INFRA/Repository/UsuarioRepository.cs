using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
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
                    .Include(u => u.UsuarioDispositivos)
                    .Include(u => u.Perfil)
                    .Include(u => u.Perfil.NavegacoesConfiguracao)
                        .ThenInclude(conf => conf.Navegacao)
                    .Include(u => u.FiltroUsuario)
                    .FirstOrDefault(u => u.CodUsuario == usuario.CodUsuario);
            }
            else
            {
                throw new Exception(Constants.USUARIO_OU_SENHA_INVALIDOS);
            }
        }

        private bool ValidateLogin(Usuario usuario)
        {
            var query = _context.Usuario.FromSqlRaw(string.Format(@"
                SELECT	    *
                FROM	    Usuario
                WHERE	    CodUsuario = '{0}'
                AND		    (PWDCompare('{1}', Senha) = 1)", usuario.CodUsuario, usuario.Senha)).ToList();

            if (query.Count > 0)
            {
                return true;
            }

            return false;
        }

        private bool PWDENCRYPT(string codUsuario, string senha)
        {
            using DbCommand command = _context.Database.GetDbConnection().CreateCommand();

            command.CommandText = string.Format("UPDATE Usuario SET Senha = PWDENCRYPT('{0}') WHERE CodUsuario = '{1}'", senha, codUsuario);
            command.CommandType = System.Data.CommandType.Text;

            _context.Database.OpenConnection();

            using DbDataReader reader = command.ExecuteReader();

            return reader.RecordsAffected == 1;
        }

        public PagedList<Usuario> ObterPorParametros(UsuarioParameters parameters)
        {
            var query = _context.Usuario
                .Include(u => u.FilialPonto)
                .Include(u => u.Cargo)
                .Include(u => u.Turno)
                .Include(u => u.Perfil)
                .Include(u => u.Filial)
                .Include(u => u.Tecnico)
                .Include(u => u.FiltroUsuario)
                .Include(u => u.Localizacoes.OrderByDescending(loc => loc.CodLocalizacao).Take(1))
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    u =>
                    u.NomeUsuario.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    u.CodUsuario.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    u.NumCracha.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodUsuario != null)
            {
                query = query.Where(u => u.CodUsuario == parameters.CodUsuario);
            }

            if (parameters.NomeUsuario != null)
            {
                query = query.Where(u => u.NomeUsuario == parameters.NomeUsuario);
            }

            if (parameters.CodPerfil != null)
            {
                query = query.Where(u => u.CodPerfil == parameters.CodPerfil);
            }

            if (parameters.CodFilial != null)
            {
                query = query.Where(u => u.CodFilial == parameters.CodFilial);
            }

            if (parameters.CodTecnico != null)
            {
                query = query.Where(u => u.CodTecnico == parameters.CodTecnico);
            }

            if (parameters.IndAtivo != null)
            {
                query = query.Where(u => u.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.CodPontoPeriodo != null)
            {
                query = query.Where(u => u.PontosPeriodoUsuario.Any(pp => pp.CodPontoPeriodo == parameters.CodPontoPeriodo));

                query = query
                    .Include(u => u.PontosPeriodoUsuario
                        .Where(pp => pp.CodPontoPeriodo == parameters.CodPontoPeriodo))
                            .ThenInclude(p => p.PontoPeriodo)
                                .ThenInclude(p => p.PontoPeriodoStatus)
                    .Include(u => u.PontosPeriodoUsuario
                        .Where(pp => pp.CodPontoPeriodo == parameters.CodPontoPeriodo))
                            .ThenInclude(p => p.PontoPeriodoUsuarioStatus);

            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Usuario>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }

        public Usuario ObterPorCodigo(string codigo)
        {
            return _context.Usuario
                .Include(u => u.Perfil)
                .Include(u => u.Cargo)
                .Include(u => u.Tecnico)
                .Include(u => u.Filial)
                .Include(u => u.Localizacoes)
                .Include(u => u.FiltroUsuario)
                .Include(u => u.Cidade)
                    .ThenInclude(u => u.UnidadeFederativa)
                    .ThenInclude(u => u.Pais)
                .FirstOrDefault(us => us.CodUsuario == codigo);
        }

        public void Atualizar(Usuario usuario)
        {
            _context.ChangeTracker.Clear();
            Usuario usr = _context.Usuario.SingleOrDefault(r => r.CodUsuario == usuario.CodUsuario);

            if (usr != null)
            {
                try
                {
                    _context.Entry(usr).CurrentValues.SetValues(usuario);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }

        public void AlterarSenha(SegurancaUsuarioModel segurancaUsuarioModel)
        {
            _context.ChangeTracker.Clear();
            Usuario usr = _context.Usuario.SingleOrDefault(r => r.CodUsuario == segurancaUsuarioModel.CodUsuario);

            if (usr != null)
            {
                try
                {
                    if (this.ValidateLogin(new Usuario() { CodUsuario = segurancaUsuarioModel.CodUsuario, Senha = segurancaUsuarioModel.SenhaAtual }))
                    {
                        if (this.PWDENCRYPT(segurancaUsuarioModel.CodUsuario, segurancaUsuarioModel.NovaSenha))
                        {
                            _context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception(Constants.ERRO_ALTERAR_SENHA);
                        }
                    }
                    else
                    {
                        throw new Exception(Constants.SENHA_INVALIDA);
                    }
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
                }
            }
        }
    }
}
