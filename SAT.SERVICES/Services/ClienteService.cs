﻿using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.SERVICES.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepo;
        private readonly ISequenciaRepository _seqRepo;

        public ClienteService(IClienteRepository clienteRepo, ISequenciaRepository seqRepo)
        {
            _clienteRepo = clienteRepo;
            _seqRepo = seqRepo;
        }

        public ListViewModel ObterPorParametros(ClienteParameters parameters)
        {
            var clientes = _clienteRepo.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = clientes,
                TotalCount = clientes.TotalCount,
                CurrentPage = clientes.CurrentPage,
                PageSize = clientes.PageSize,
                TotalPages = clientes.TotalPages,
                HasNext = clientes.HasNext,
                HasPrevious = clientes.HasPrevious
            };

            return lista;
        }

        public Cliente Criar(Cliente cliente)
        {
            cliente.CodCliente = _seqRepo.ObterContador("Cliente");
            _clienteRepo.Criar(cliente);
            return cliente;
        }

        public void Deletar(int codigo)
        {
            _clienteRepo.Deletar(codigo);
        }

        public void Atualizar(Cliente cliente)
        {
            _clienteRepo.Atualizar(cliente);
        }

        public Cliente ObterPorCodigo(int codigo)
        {
            return _clienteRepo.ObterPorCodigo(codigo);
        }
    }
}
