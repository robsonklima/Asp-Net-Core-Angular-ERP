using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using SAT.MODELS.ViewModels;
using System;
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
                    .Include(u => u.UsuarioSeguranca)
                    .Include(u => u.UsuarioDispositivos)
                    .Include(u => u.Setor)
                    .Include(u => u.Perfil)
                    .Include(u => u.NavegacoesConfiguracao)
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
                .Include(u => u.Setor)
                .Include(u => u.RecursosBloqueados)
                .Include(u => u.Filial)
                .Include(u => u.Tecnico)
                .Include(u => u.FiltroUsuario)
                .Include(u => u.UsuarioSeguranca)
                .Include(u => u.PontosPeriodoUsuario)
                    .ThenInclude(u => u.PontoPeriodo)
                        .ThenInclude(u => u.PontoPeriodoStatus)
                .Include(u => u.PontosPeriodoUsuario)
                    .ThenInclude(u => u.PontoPeriodoUsuarioStatus)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    u =>
                    u.NomeUsuario.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    u.CodUsuario.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    u.NumCracha.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    u.Perfil.NomePerfil.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    u.Cargo.NomeCargo.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    u.Filial.NomeFilial.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodCargos))
            {
                int[] cods = parameters.CodCargos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(dc => cods.Contains(dc.CodCargo.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodPerfis))
            {
                int[] cods = parameters.CodPerfis.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(dc => cods.Contains(dc.CodPerfil.Value));
            }

            if (parameters.CodUsuario != null)
            {
                query = query.Where(u => u.CodUsuario == parameters.CodUsuario);
            }

            if (parameters.NomeUsuario != null)
            {
                query = query.Where(u => u.NomeUsuario == parameters.NomeUsuario);
            }

            if (parameters.CodFilial != null)
            {
                query = query.Where(u => u.CodFilial == parameters.CodFilial);
            }

            if (parameters.CodSetor != null)
            {
                query = query.Where(u => u.CodSetor == parameters.CodSetor);
            }

            if (parameters.CodTecnico != null)
            {
                query = query.Where(u => u.CodTecnico == parameters.CodTecnico);
            }

            if (parameters.IndAtivo != null)
            {
                query = query.Where(u => u.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.IndFerias != null)
            {
                query = query.Where(u => u.Tecnico.IndFerias == parameters.IndFerias);
            }

            if (parameters.IndPonto != null)
            {
                query = query.Where(u => u.IndPonto == parameters.IndPonto);
            }

            if (!string.IsNullOrWhiteSpace(parameters.PAS))
            {
                int[] pas = parameters.PAS.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(u => pas.Contains(u.Tecnico.RegiaoAutorizada.PA.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodTecnicos))
            {
                int[] codTecnicos = parameters.CodTecnicos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(u => codTecnicos.Contains(u.Tecnico.CodTecnico.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                int[] filiais = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(u => filiais.Contains(u.CodFilial.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodUsuarios))
            {
                var usuarios = parameters.CodUsuarios.Split(',').Select(e => e.Trim());
                query = query.Where(u => usuarios.Any(p => p == u.CodUsuario));
            }

            if (parameters.CodPontoPeriodoUsuarioStatus.HasValue && parameters.CodPontoPeriodo.HasValue)
            {
                query = query
                    .Where(p => p.PontosPeriodoUsuario
                        .Any(p => p.CodPontoPeriodoUsuarioStatus == parameters.CodPontoPeriodoUsuarioStatus && p.CodPontoPeriodo == parameters.CodPontoPeriodo))
                    .Include(u => u.PontosPeriodoUsuario
                    .Where(p => p.CodPontoPeriodoUsuarioStatus == parameters.CodPontoPeriodoUsuarioStatus && p.CodPontoPeriodo == parameters.CodPontoPeriodo));
            }
            else if (parameters.CodPontoPeriodo != null)
            {
                query = query
                    .Include(u => u.PontosPeriodoUsuario
                    .Where(p => p.CodPontoPeriodo == parameters.CodPontoPeriodo));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodPerfisNotIn))
            {
                int[] perfis = parameters.CodPerfisNotIn.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(u => !perfis.Contains(u.CodPerfil.Value));
            }

            if (parameters.UltimoAcessoInicio.HasValue)
                query = query.Where(r => r.UltimoAcesso >= parameters.UltimoAcessoInicio);

            if (parameters.UltimoAcessoFim.HasValue)
                query = query.Where(r => r.UltimoAcesso <= parameters.UltimoAcessoFim);

            if (parameters.PontoInicio.HasValue && parameters.PontoFim.HasValue)
            {
                query = query.Include(u => u.PontosUsuario.Where(r => r.DataHoraRegistro.Date >= parameters.PontoInicio.Value.Date &&
                    r.DataHoraRegistro.Date <= parameters.PontoFim.Value.Date));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Usuario>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }

        public Usuario ObterPorCodigo(string codigo)
        {
            return _context.Usuario
                .Include(u => u.Perfil)
                .Include(u => u.Setor)
                .Include(u => u.RecursosBloqueados)
                .Include(u => u.Cargo)
                .Include(u => u.Tecnico)
                .Include(u => u.Filial)
                .Include(u => u.Localizacoes)
                .Include(u => u.FiltroUsuario)
                .Include(u => u.UsuarioSeguranca)
                .Include(u => u.Cidade!)
                .FirstOrDefault(us => us.CodUsuario == codigo);
        }

        public Usuario ObterLoginController(string codigo)
        {
            return _context.Usuario
                .FirstOrDefault(us => us.CodUsuario == codigo);
        }


        public void Criar(Usuario usuario)
        {
            try
            {
                _context.Add(usuario);
                _context.SaveChanges();

                if (!this.PWDENCRYPT(usuario.CodUsuario, usuario.Senha))
                {
                    throw new Exception(Constants.ERRO_ALTERAR_SENHA);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
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
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }

        public void AlterarSenha(SegurancaUsuarioModel segurancaUsuarioModel, bool forcaTrocarSenha = false)
        {
            Usuario usr = _context.Usuario.SingleOrDefault(r => r.CodUsuario == segurancaUsuarioModel.CodUsuario);

            if (usr != null)
            {
                try
                {
                    if (this.ValidateLogin(new Usuario() { CodUsuario = segurancaUsuarioModel.CodUsuario, Senha = segurancaUsuarioModel.SenhaAtual }) || forcaTrocarSenha)
                    {
                        if (this.PWDENCRYPT(segurancaUsuarioModel.CodUsuario, segurancaUsuarioModel.NovaSenha))
                        {
                            _context.ChangeTracker.Clear();
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
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }

        public RecuperaSenha CriarRecuperaSenha(RecuperaSenha recuperaSenha)
        {
            try
            {
                _context.Add(recuperaSenha);
                _context.SaveChanges();

                return recuperaSenha;
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void AtualizarRecuperaSenha(RecuperaSenha recuperaSenha)
        {
            RecuperaSenha usr = _context.RecuperaSenha.FirstOrDefault(r => r.CodRecuperaSenha == recuperaSenha.CodRecuperaSenha);

            try
            {
                if (usr != null)
                {
                    _context.Entry(usr).CurrentValues.SetValues(recuperaSenha);
                    _context.ChangeTracker.Clear();
                    _context.SaveChanges();
                }
                else
                {
                    usr = _context.RecuperaSenha.FirstOrDefault(r => r.CodUsuario == recuperaSenha.CodUsuario && r.IndAtivo == 1);
                    if (usr != null)
                    {
                        usr.IndAtivo = 0;
                        _context.ChangeTracker.Clear();
                        _context.SaveChanges();
                    }
                }
            }
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
            }
        }

        public RecuperaSenha ObterRecuperaSenha(int codRecuperaSenha)
        {
            return _context.RecuperaSenha.FirstOrDefault(us => us.CodRecuperaSenha == codRecuperaSenha);
        }

        public void DesbloquearAcesso(string codUsuario)
        {
            _context.ChangeTracker.Clear();
            UsuarioSeguranca usrSeguranca = _context.UsuarioSeguranca.SingleOrDefault(r => r.CodUsuario == codUsuario);

            if (usrSeguranca != null)
            {
                try
                {
                    usrSeguranca.QuantidadeTentativaLogin = 0;
                    usrSeguranca.SenhaBloqueada = 0;
                    usrSeguranca.SenhaExpirada = 0;
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }
    }
}
