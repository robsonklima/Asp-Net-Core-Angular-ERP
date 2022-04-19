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
    public class ClienteBancadaRepository : IClienteBancadaRepository
    {
        private readonly AppDbContext _context;

        public ClienteBancadaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(ClienteBancada clienteBancada)
        {
            _context.ChangeTracker.Clear();
            ClienteBancada c = _context.ClienteBancada.FirstOrDefault(c => c.CodClienteBancada == clienteBancada.CodClienteBancada);

            if (c != null)
            {
                _context.Entry(c).CurrentValues.SetValues(clienteBancada);
                _context.Entry(c).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Criar(ClienteBancada clienteBancada)
        {
            _context.Add(clienteBancada);
            _context.SaveChanges();
        }

        public void Deletar(int codClienteBancada)
        {
            ClienteBancada c = _context.ClienteBancada.FirstOrDefault(c => c.CodClienteBancada == codClienteBancada);

            if (c != null)
            {
                _context.ClienteBancada.Remove(c);
                _context.SaveChanges();
            }
        }

        public ClienteBancada ObterPorCodigo(int codigo)
        {
            return _context.ClienteBancada
                .Include(i => i.Cidade)
                     .ThenInclude(i => i.UnidadeFederativa)
                      .ThenInclude(i => i.Pais)
                .FirstOrDefault(c => c.CodClienteBancada == codigo);
        }

        public PagedList<ClienteBancada> ObterPorParametros(ClienteBancadaParameters parameters)
        {
            IQueryable<ClienteBancada> clientBancadas = _context.ClienteBancada
               .Include(i => i.Cidade)
                     .ThenInclude(i => i.UnidadeFederativa)
                      .ThenInclude(i => i.Pais)
                .AsQueryable();

            if (parameters.IndAtivo != null)
            {
                clientBancadas = clientBancadas.Where(p => p.IndAtivo == parameters.IndAtivo.Value);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                clientBancadas = clientBancadas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<ClienteBancada>.ToPagedList(clientBancadas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
