using System.Linq;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Helpers;

namespace SAT.INFRA.Repository
{
    public class UsuarioLoginRepository : IUsuarioLoginRepository
    {
        private readonly AppDbContext _context;

        public UsuarioLoginRepository(AppDbContext context)
        {
            _context = context;
        }

        public UsuarioLogin Criar(UsuarioLogin login)
        {
            _context.Add(login);
            _context.SaveChanges();
            return login;
        }

        public PagedList<UsuarioLogin> ObterPorParametros(UsuarioLoginParameters parameters)
        {
            var query = _context.UsuarioLogin
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(
                    t =>
                    t.CodUsuario.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodUsuario != null)
                query = query.Where(u => u.CodUsuario == parameters.CodUsuario);

            if (parameters.DataHoraCadInicio.HasValue)
                query = query.Where(r => r.DataHoraCad.Date >= parameters.DataHoraCadInicio.Value.Date);

            if (parameters.DataHoraCadFim.HasValue)
                query = query.Where(r => r.DataHoraCad.Date >= parameters.DataHoraCadFim.Value.Date);

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<UsuarioLogin>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
