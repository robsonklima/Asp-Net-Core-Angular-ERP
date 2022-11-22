using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class LaudoRepository : ILaudoRepository
    {
        private readonly AppDbContext _context;

        public LaudoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Laudo laudo)
        {
            _context.ChangeTracker.Clear();
            Laudo l = _context.Laudo
                .FirstOrDefault(l => l.CodLaudo == laudo.CodLaudo);
            try
            {
                if (l != null)
                {
                    _context.Entry(l).CurrentValues.SetValues(laudo);
                    _context.SaveChanges();
                }
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public Laudo ObterPorCodigo(int codigo)
        {
            try
            {
                return _context.Laudo
                .Include(l => l.LaudosSituacao)
                .Include(l => l.LaudoStatus)
                .Include(l => l.Or)
                .Include(l => l.Or)
                    .ThenInclude(l => l.Cliente)
                .Include(l => l.Or)
                    .ThenInclude(l => l.Equipamento)
                .Include(l => l.Or)
                    .ThenInclude(l => l.TipoIntervencao)
                .Include(l => l.Or)
                    .ThenInclude(l => l.LocalAtendimento)
                .Include(l => l.Tecnico)
                .SingleOrDefault(a => a.CodLaudo == codigo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a auditoria {ex.Message}");
            }
        }

        public PagedList<Laudo> ObterPorParametros(LaudoParameters parameters)
        {
            var laudos = _context.Laudo
                .Include(l => l.LaudosSituacao)
                .Include(l => l.LaudoStatus)
                .Include(l => l.Or)
                .Include(l => l.Or)
                    .ThenInclude(l => l.Cliente)
                .Include(l => l.Or)
                    .ThenInclude(l => l.Equipamento)
                .Include(l => l.Or)
                    .ThenInclude(l => l.TipoIntervencao)
                .Include(l => l.Or)
                    .ThenInclude(l => l.LocalAtendimento)
                .Include(l => l.Tecnico)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                laudos = laudos.Where(a =>
                    a.CodLaudo.ToString().Contains(parameters.Filter) ||
                    a.Tecnico.Nome.Contains(parameters.Filter) ||
                    a.LaudoStatus.NomeStatus.Contains(parameters.Filter));
            }

            if (parameters.CodOS.HasValue)
                laudos = laudos.Where(l => l.Or.CodOS == parameters.CodOS.Value);

            if (parameters.CodTecnico.HasValue)
                laudos = laudos.Where(l => l.Tecnico.CodTecnico == parameters.CodTecnico.Value);

            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                var clientes = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                laudos = laudos.Where(l => clientes.Any(p => p == l.Or.Cliente.CodCliente));
            }

            if (!string.IsNullOrEmpty(parameters.CodEquips))
            {
                var modelos = parameters.CodEquips.Split(',').Select(e => e.Trim());
                laudos = laudos.Where(l => modelos.Any(p => p == l.Or.Equipamento.CodEquip.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodTecnicos))
            {
                var usuarios = parameters.CodTecnicos.Split(',').Select(e => e.Trim());
                laudos = laudos.Where(l => usuarios.Any(p => p == l.Tecnico.CodTecnico.ToString()));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                laudos = laudos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Laudo>.ToPagedList(laudos, parameters.PageNumber, parameters.PageSize);
        }

    }
}