using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TurnoRepository : ITurnoRepository
    {
        private readonly AppDbContext _context;

        public TurnoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Turno turno)
        {
            _context.ChangeTracker.Clear();
            Turno t = _context.Turno.FirstOrDefault(t => t.CodTurno == turno.CodTurno);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(turno);
                _context.SaveChanges();
            }
        }

        public void Criar(Turno turno)
        {
            _context.Add(turno);
            _context.SaveChanges();
        }

        public void Deletar(int codTurno)
        {
            Turno t = _context.Turno.FirstOrDefault(t => t.CodTurno == codTurno);

            if (t != null)
            {
                _context.Turno.Remove(t);
                _context.SaveChanges();
            }
        }

        public Turno ObterPorCodigo(int codigo)
        {
            return _context.Turno.FirstOrDefault(t => t.CodTurno == codigo);
        }

        public PagedList<Turno> ObterPorParametros(TurnoParameters parameters)
        {
            var turnos = _context.Turno
                .AsQueryable();

            if (parameters.Filter != null)
            {
                turnos = turnos.Where(
                    t =>
                    t.DescTurno.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.CodTurno.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                turnos = turnos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Turno>.ToPagedList(turnos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
