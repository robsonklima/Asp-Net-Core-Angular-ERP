using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class LiderTecnicoRepository : ILiderTecnicoRepository
    {
        private readonly AppDbContext _context;

        public LiderTecnicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(LiderTecnico liderTecnico)
        {
            _context.ChangeTracker.Clear();
            LiderTecnico c = _context.LiderTecnico.FirstOrDefault(c => c.CodLiderTecnico == liderTecnico.CodLiderTecnico);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(liderTecnico);
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Criar(LiderTecnico liderTecnico)
        {
            _context.Add(liderTecnico);
            _context.SaveChanges();
        }

        public void Deletar(int codLiderTecnico)
        {
            LiderTecnico c = _context.LiderTecnico.FirstOrDefault(c => c.CodLiderTecnico == codLiderTecnico);

            if (c != null)
            {
                _context.LiderTecnico.Remove(c);
                _context.SaveChanges();
            }
        }

        public LiderTecnico ObterPorCodigo(int codigo)
        {
            return _context.LiderTecnico
                .Include(i => i.UsuarioLider)
                .Include(i => i.Tecnico)
                .FirstOrDefault(c => c.CodLiderTecnico == codigo);
        }

        public PagedList<LiderTecnico> ObterPorParametros(LiderTecnicoParameters parameters)
        {
            IQueryable<LiderTecnico> lidTecnico = _context.LiderTecnico
                .Include(i => i.UsuarioLider)
                .Include(i => i.Tecnico)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                lidTecnico = lidTecnico.Where(
                            l =>
                            l.CodLiderTecnico.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            l.CodUsuarioLider.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            l.UsuarioLider.NomeUsuario.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                            l.Tecnico.Nome.Contains(parameters.Filter)
                );
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodUsuarioLideres))
            {
                string[] cods = parameters.CodUsuarioLideres.Split(",").Select(a => a.Trim()).ToArray();
                lidTecnico = lidTecnico.Where(dc => cods.Contains(dc.CodUsuarioLider));
            }


            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                lidTecnico = lidTecnico.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<LiderTecnico>.ToPagedList(lidTecnico, parameters.PageNumber, parameters.PageSize);
        }
    }
}
