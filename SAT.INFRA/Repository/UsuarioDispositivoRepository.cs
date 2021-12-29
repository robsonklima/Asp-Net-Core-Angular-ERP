using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class UsuarioDispositivoRepository : IUsuarioDispositivoRepository
    {
        private readonly AppDbContext _context;

        public UsuarioDispositivoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(UsuarioDispositivo usuarioDispositivo)
        {
            UsuarioDispositivo d = _context.UsuarioDispositivo
                .FirstOrDefault(d => d.CodUsuarioDispositivo == usuarioDispositivo.CodUsuarioDispositivo);

            if (d != null)
            {
                _context.Entry(d).CurrentValues.SetValues(usuarioDispositivo);
                _context.SaveChanges();
            }
        }

        public UsuarioDispositivo Criar(UsuarioDispositivo usuarioDispositivo)
        {
            _context.Add(usuarioDispositivo);
            _context.SaveChanges();
            return usuarioDispositivo;
        }

        public UsuarioDispositivo ObterPorCodigo(int codigo)
        {
            return _context.UsuarioDispositivo.FirstOrDefault(t => t.CodUsuarioDispositivo == codigo);
        }

        public PagedList<UsuarioDispositivo> ObterPorParametros(UsuarioDispositivoParameters parameters)
        {
            var dispositivos = _context.UsuarioDispositivo
                .AsQueryable();

            if (parameters.CodUsuario != null)
            {
                dispositivos = dispositivos.Where(d => d.CodUsuario == parameters.CodUsuario);
            }

            if (parameters.SistemaOperacional != null)
            {
                dispositivos = dispositivos.Where(d => d.SistemaOperacional == parameters.SistemaOperacional);
            }

            if (parameters.Navegador != null)
            {
                dispositivos = dispositivos.Where(d => d.Navegador == parameters.Navegador);
            }

            if (parameters.VersaoNavegador != null)
            {
                dispositivos = dispositivos.Where(d => d.VersaoNavegador == parameters.VersaoNavegador);
            }

            if (parameters.VersaoSO != null)
            {
                dispositivos = dispositivos.Where(d => d.VersaoSO == parameters.VersaoSO);
            }

            if (parameters.TipoDispositivo != null)
            {
                dispositivos = dispositivos.Where(d => d.TipoDispositivo == parameters.TipoDispositivo);
            }

            if (parameters.Ip != null)
            {
                dispositivos = dispositivos.Where(u => u.Ip == parameters.Ip);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                dispositivos = dispositivos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<UsuarioDispositivo>.ToPagedList(dispositivos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
