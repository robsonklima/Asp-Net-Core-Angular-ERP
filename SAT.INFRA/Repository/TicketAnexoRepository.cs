using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class TicketAnexoRepository : ITicketAnexoRepository
    {
        private readonly AppDbContext _context;

        public TicketAnexoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<TicketAnexo> ObterPorParametros(TicketAnexoParameters parameters)
        {
            var query = _context.TicketAnexo
                .Include(t => t.UsuarioCad)
                .AsQueryable();


            if (parameters.SortActive != null && parameters.SortDirection != null)
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<TicketAnexo>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
        
        public TicketAnexo ObterPorCodigo(int codTicketAnexo)
        {
            return _context.TicketAnexo
                .Include(t => t.UsuarioCad)
                .FirstOrDefault(t => t.CodTicketAnexo == codTicketAnexo);
        }

        public TicketAnexo Atualizar(TicketAnexo anexo)
        {
            _context.ChangeTracker.Clear();
            TicketAnexo t = _context.TicketAnexo.FirstOrDefault(t => t.CodTicketAnexo == anexo.CodTicketAnexo);

            if (t != null)
            {
                _context.Entry(t).CurrentValues.SetValues(anexo);
                _context.SaveChanges();
            }

            return t;
        }

        public TicketAnexo Criar(TicketAnexo anexo)
        {
            _context.Add(anexo);
            _context.SaveChanges();
            return anexo;
        }

        public TicketAnexo Deletar(int cod)
        {
            TicketAnexo anexo = _context.TicketAnexo
                .FirstOrDefault(t => t.CodTicketAnexo == cod);

            if (anexo != null)
            {
                _context.TicketAnexo.Remove(anexo);
                _context.SaveChanges();
            }

            return anexo;
        }
    }
}