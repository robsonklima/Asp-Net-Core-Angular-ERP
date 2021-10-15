using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Collections.Generic;

namespace SAT.INFRA.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context;
        }

        public Cliente ObterPorCodigo(int codigo)
        {
            return _context.Cliente
                .FirstOrDefault(c => c.CodCliente == codigo);
        }

        public PagedList<Cliente> ObterPorParametros(ClienteParameters parameters)
        {
            var clientes = _context.Cliente.AsQueryable();

            if (parameters.Filter != null)
            {
                clientes = clientes.Where(
                    s =>
                    s.CodCliente.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.NomeFantasia.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    s.NumBanco.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodCliente != null)
            {
                clientes = clientes.Where(c => c.CodCliente == parameters.CodCliente);
            }

            if (parameters.IndAtivo != null)
            {
                clientes = clientes.Where(c => c.IndAtivo == parameters.IndAtivo);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                clientes = clientes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Cliente>.ToPagedList(clientes, parameters.PageNumber, parameters.PageSize);
        }

        public IEnumerable<Cliente> ObterTodos()
        {
            return _context.Cliente;
        }
    }
}
