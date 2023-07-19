using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;

namespace SAT.INFRA.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;
        private readonly ISequenciaRepository _sequenciaRepository;

        public ClienteRepository(AppDbContext context, ISequenciaRepository sequenciaRepository)
        {
            _context = context;
            this._sequenciaRepository = sequenciaRepository;
        }
        public void Atualizar(Cliente cliente)
        {
            try
            {
                _context.ChangeTracker.Clear();
                Cliente cl = _context.Cliente.FirstOrDefault(c => c.CodCliente == cliente.CodCliente);

                if (cl != null)
                {
                    _context.Entry(cl).CurrentValues.SetValues(cliente);
                    _context.Entry(cl).State = EntityState.Modified;
                    _context.SaveChanges();
                }
            }
            catch
            {
                throw;
            }
        }

        public void Criar(Cliente cliente)
        {
            try
            {
                _context.Add(cliente);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codCliente)
        {
            Cliente cl = _context.Cliente.FirstOrDefault(c => c.CodCliente == codCliente);

            if (cl != null)
            {
                _context.Cliente.Remove(cl);
                _context.SaveChanges();
            }
        }

        public Cliente ObterPorCodigo(int codigo)
        {
            return _context.Cliente
                .Include(i => i.Cidade)
                 .ThenInclude(i => i.UnidadeFederativa)
                    .ThenInclude(i => i.Pais)
                .Include(i => i.Transportadora)
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
                clientes = clientes.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            if (!string.IsNullOrWhiteSpace(parameters.NomeFantasia))
                clientes = clientes.Where(c => c.NomeFantasia == parameters.NomeFantasia);

            return PagedList<Cliente>.ToPagedList(clientes, parameters.PageNumber, parameters.PageSize);
        }

        public IQueryable<Cliente> ObterPorQuery(ClienteParameters parameters)
        {
            var clientes = _context.Cliente.AsQueryable();

            if (parameters.IndAtivo.HasValue)
                clientes = clientes.Where(c => c.IndAtivo == parameters.IndAtivo);

            return clientes;
        }

        public IEnumerable<Cliente> ObterTodos()
        {
            return _context.Cliente;
        }
    }
}
