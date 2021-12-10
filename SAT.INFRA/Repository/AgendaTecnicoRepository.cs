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
        public AgendaTecnico Atualizar(AgendaTecnico agenda)
        {
            AgendaTecnico a = _context.AgendaTecnico.SingleOrDefault(a => a.CodAgendaTecnico == agenda.CodAgendaTecnico);
            try
            {
                if (a != null)
                {
                    _context.Entry(a).CurrentValues.SetValues(agenda);
                    _context.SaveChanges();
                    return agenda;
                }
                return null;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AgendaTecnico Criar(AgendaTecnico agenda)
        {
            try
            {
                _context.Add(agenda);
                _context.SaveChanges();
                return agenda;
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
            var agendas = this.ObterQuery(parameters);

            return PagedList<AgendaTecnico>.ToPagedList(agendas, parameters.PageNumber, parameters.PageSize);
        }

        public IQueryable<AgendaTecnico> ObterQuery(AgendaTecnicoParameters parameters)
        {
            var agendas = _context.AgendaTecnico.AsQueryable();

            if (parameters.InicioPeriodoAgenda.HasValue && parameters.FimPeriodoAgenda.HasValue)
                agendas = agendas.Where(ag => ag.Inicio.Date >= parameters.InicioPeriodoAgenda.Value.Date && ag.Fim.Date <= parameters.FimPeriodoAgenda.Value.Date);

            if (parameters.Tipo.HasValue)
                agendas = agendas.Where(ag => ag.Tipo == parameters.Tipo);

            if (parameters.CodTecnico.HasValue)
                agendas = agendas.Where(ag => ag.CodTecnico == parameters.CodTecnico);

            return agendas;
        }
    }
}
