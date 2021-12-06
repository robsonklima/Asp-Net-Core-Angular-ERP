using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class AgendaTecnicoRepository : IAgendaTecnicoRepository
    {
        private readonly AppDbContext _context;

        public AgendaTecnicoRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Atualizar(AgendaTecnico agenda)
        {
            AgendaTecnico a = _context.AgendaTecnico.SingleOrDefault(a => a.CodAgendaTecnico == agenda.CodAgendaTecnico);

            if (a != null)
            {
                _context.Entry(a).CurrentValues.SetValues(agenda);
                _context.SaveChanges();
            }
        }

        public void Criar(AgendaTecnico agenda)
        {
            try
            {
                _context.Add(agenda);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Deletar(int codigo)
        {
            AgendaTecnico a = _context.AgendaTecnico.SingleOrDefault(a => a.CodAgendaTecnico == codigo);

            if (a != null)
            {
                try
                {
                    _context.AgendaTecnico.Remove(a);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public AgendaTecnico ObterPorCodigo(int codigo)
        {
            var agendas = _context.AgendaTecnico.AsQueryable();

            return agendas.SingleOrDefault(a => a.CodAgendaTecnico == codigo);
        }

        public PagedList<AgendaTecnico> ObterPorParametros(AgendaTecnicoParameters parameters)
        {
            var agendas = _context.AgendaTecnico
            .Include(ag => ag.Tecnico)
            .Include(ag => ag.OrdemServico)
            .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                agendas = agendas.Where(
                    a =>
                    a.CodAgendaTecnico.ToString().Contains(parameters.Filter) ||
                    a.CodOS.ToString().Contains(parameters.Filter) ||
                    a.CodTecnico.ToString().Contains(parameters.Filter));

            if (parameters.CodOS.HasValue)
                agendas = agendas.Where(a => a.CodOS == parameters.CodOS);

            if (parameters.CodTecnico.HasValue)
                agendas = agendas.Where(a => a.CodTecnico == parameters.CodTecnico);

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(",");
                agendas = agendas.Where(ag => filiais.Any(a => a == ag.Tecnico.CodFilial.ToString()));
            }

            if (parameters.InicioPeriodoAgenda.HasValue && parameters.FimPeriodoAgenda.HasValue)
                agendas = agendas.Where(ag => ag.Inicio.Date >= parameters.InicioPeriodoAgenda.Value.Date && ag.Fim.Date <= parameters.FimPeriodoAgenda.Value.Date);

            if (!string.IsNullOrEmpty(parameters.Tipo))
                agendas = agendas.Where(ag => ag.Tipo.ToLower() == parameters.Tipo.ToLower());

            return PagedList<AgendaTecnico>.ToPagedList(agendas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
