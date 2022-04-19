using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System;
using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.INFRA.Repository
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly AppDbContext _context;

        public AgendamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Agendamento agendamento)
        {
            _context.ChangeTracker.Clear();
            Agendamento a = _context.Agendamento.SingleOrDefault(a => a.CodAgendamento == agendamento.CodAgendamento);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(agendamento);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public void Criar(Agendamento agendamento)
        {
            try
            {
                _context.Add(agendamento);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            Agendamento a = _context.Agendamento.SingleOrDefault(a => a.CodAgendamento == codigo);

            if (a != null)
            {
                try
                {
                    _context.Agendamento.Remove(a);
                    _context.SaveChanges();
                }
                catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
            }
        }

        public Agendamento ObterPorCodigo(int codigo)
        {
            return _context.Agendamento.SingleOrDefault(a => a.CodAgendamento == codigo);
        }

        public PagedList<Agendamento> ObterPorParametros(AgendamentoParameters parameters)
        {
            var agendamentos = _context.Agendamento.AsQueryable();

            if (parameters.CodAgendamento.HasValue)
                agendamentos = agendamentos.Where(a => a.CodAgendamento == parameters.CodAgendamento);

            if (parameters.CodOS.HasValue)
                agendamentos = agendamentos.Where(a => a.CodOS == parameters.CodOS);

            if (!string.IsNullOrWhiteSpace(parameters.SortActive) && !string.IsNullOrWhiteSpace(parameters.SortDirection))
                agendamentos = agendamentos.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<Agendamento>.ToPagedList(agendamentos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
