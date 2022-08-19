using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using System.Linq.Dynamic.Core;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class ConferenciaRepository : IConferenciaRepository
    {
        private readonly AppDbContext _context;

        public ConferenciaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Conferencia conferencia)
        {
            _context.ChangeTracker.Clear();
            Conferencia c = _context.Conferencia.FirstOrDefault(c => c.CodConferencia == conferencia.CodConferencia);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(conferencia);
                _context.SaveChanges();
            }
        }

        public void Criar(Conferencia conferencia)
        {
            _context.Add(conferencia);
            _context.SaveChanges();
        }

        public void Deletar(int codConferencia)
        {
            Conferencia c = _context.Conferencia.FirstOrDefault(c => c.CodConferencia == codConferencia);

            if (c != null)
            {
                _context.Conferencia.Remove(c);
                _context.SaveChanges();
            }
        }

        
        public Conferencia ObterPorCodigo(int codigo)
        {
            return _context.Conferencia
                .Include(c => c.UsuarioCadastro)
                .Include(c => c.UsuarioManut)
                .FirstOrDefault(c => c.CodConferencia == codigo);
        }

        public PagedList<Conferencia> ObterPorParametros(ConferenciaParameters parameters)
        {
            var Conferencias = _context.Conferencia
                .Include(c => c.UsuarioCadastro)
                .Include(c => c.UsuarioManut)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                Conferencias = Conferencias.Where(
                    s =>
                    s.CodConferencia.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            
            if (parameters.SortActive != null && parameters.SortDirection != null)
                 Conferencias = Conferencias.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<Conferencia>.ToPagedList(Conferencias, parameters.PageNumber, parameters.PageSize);
        }
    }
}
