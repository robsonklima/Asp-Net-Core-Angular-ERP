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
        public void AtualizarAgenda(AgendaTecnico agenda)
        {
            AgendaTecnico a = _context.AgendaTecnico.SingleOrDefault(a => a.Id == agenda.Id);

            if (a != null)
            {
                try
                {
                    _context.Entry(a).CurrentValues.SetValues(agenda);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void AtualizarEvento(AgendaTecnicoEvento evento)
        {
            AgendaTecnicoEvento e = _context.AgendaTecnicoEvento.SingleOrDefault(e => e.Id == evento.Id);

            if (e != null)
            {
                try
                {
                    _context.Entry(e).CurrentValues.SetValues(evento);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void CriarAgenda(AgendaTecnico agenda)
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

        public void CriarEvento(AgendaTecnicoEvento evento)
        {
            try
            {
                _context.Add(evento);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeletarAgenda(int codigo)
        {
            AgendaTecnico a = _context.AgendaTecnico.SingleOrDefault(a => a.Id == codigo);

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

        public void DeletarEvento(int codigo)
        {
            AgendaTecnicoEvento e = _context.AgendaTecnicoEvento.SingleOrDefault(e => e.Id == codigo);

            if (e != null)
            {
                try
                {
                    _context.AgendaTecnicoEvento.Remove(e);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public AgendaTecnico ObterAgendaPorCodigo(int codigo)
        {
            return _context.AgendaTecnico.SingleOrDefault(a => a.Id == codigo);
        }

        public PagedList<AgendaTecnico> ObterAgendaPorParametros(AgendaTecnicoParameters parameters)
        {
            var agendas = _context.AgendaTecnico
                .Include(a => a.Tecnico)
                .Include(a => a.Eventos)
                .AsQueryable();

            if (parameters.CodTecnico != null)
            {
                agendas = agendas.Where(a => a.CodTecnico == parameters.CodTecnico);
            }

            if (parameters.CodFilial != null)
            {
                agendas = agendas.Where(a => a.Tecnico.CodFilial == parameters.CodFilial);
            }

            if (parameters.Filter != null)
            {
                agendas = agendas.Where(
                    a =>
                    a.Id.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    a.Title.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            return PagedList<AgendaTecnico>.ToPagedList(agendas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
