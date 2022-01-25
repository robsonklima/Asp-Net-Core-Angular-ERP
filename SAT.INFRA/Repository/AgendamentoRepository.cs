using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System;
using System.Linq;

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
            Agendamento a = _context.Agendamento.SingleOrDefault(a => a.CodAgendamento == agendamento.CodAgendamento);

            if (a != null)
            {
                try
                {
                    _context.ChangeTracker.Clear();
                    _context.Entry(a).CurrentValues.SetValues(agendamento);
                    _context.SaveChanges();
                }
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_ATUALIZAR);
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
            catch (DbUpdateException)
            {
                throw new Exception(Constants.NAO_FOI_POSSIVEL_CRIAR);
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
                catch (DbUpdateException)
                {
                    throw new Exception(Constants.NAO_FOI_POSSIVEL_DELETAR);
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
                agendamentos = agendamentos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<Agendamento>.ToPagedList(agendamentos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
