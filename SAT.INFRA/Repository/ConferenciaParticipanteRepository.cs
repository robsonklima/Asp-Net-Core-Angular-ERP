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
    public class ConferenciaParticipanteRepository : IConferenciaParticipanteRepository
    {
        private readonly AppDbContext _context;

        public ConferenciaParticipanteRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Criar(ConferenciaParticipante ConferenciaParticipante)
        {
            _context.Add(ConferenciaParticipante);
            _context.SaveChanges();
        }

        public void Deletar(int codigo)
        {
            ConferenciaParticipante c = _context.ConferenciaParticipante.FirstOrDefault(c => c.CodConferenciaParticipante == codigo);

            if (c != null)
            {
                _context.ConferenciaParticipante.Remove(c);
                _context.SaveChanges();
            }
        }
        
        public ConferenciaParticipante ObterPorCodigo(int codigo)
        {
            return _context.ConferenciaParticipante
                .Include(c => c.UsuarioCadastro)
                .Include(c => c.UsuarioParticipante)
                .FirstOrDefault(c => c.CodConferenciaParticipante == codigo);
        }

        public PagedList<ConferenciaParticipante> ObterPorParametros(ConferenciaParticipanteParameters parameters)
        {
            var ConferenciaParticipantes = _context.ConferenciaParticipante
                .Include(c => c.UsuarioCadastro)
                .Include(c => c.UsuarioParticipante)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                ConferenciaParticipantes = ConferenciaParticipantes.Where(
                    s =>
                    s.CodConferenciaParticipante.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            
            if (parameters.SortActive != null && parameters.SortDirection != null)
                 ConferenciaParticipantes = ConferenciaParticipantes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<ConferenciaParticipante>.ToPagedList(ConferenciaParticipantes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
