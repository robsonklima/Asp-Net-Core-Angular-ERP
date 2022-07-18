using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class AgendaTecnicoRepository : IAgendaTecnicoRepository
    {
        private readonly AppDbContext _context;

        public AgendaTecnicoRepository(AppDbContext context)
        {
            _context = context;
        }
        public AgendaTecnico Atualizar(AgendaTecnico agenda)
        {
            _context.ChangeTracker.Clear();
            AgendaTecnico ag = _context.AgendaTecnico.SingleOrDefault(a => a.CodAgendaTecnico == agenda.CodAgendaTecnico);
            try
            {
                if (ag != null)
                {
                    _context.Entry(ag).CurrentValues.SetValues(agenda);
                    _context.SaveChanges();
                    return agenda;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
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
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public AgendaTecnico ObterPorCodigo(int codigo)
        {
            var agendas = _context.AgendaTecnico.AsQueryable();

            return agendas.SingleOrDefault(a => a.CodAgendaTecnico == codigo);
        }

        public List<ViewAgendaTecnicoEvento> ObterViewPorParametros(AgendaTecnicoParameters parameters)
        {
            var agendas = _context.ViewAgendaTecnicoEvento.AsQueryable();

            if (parameters.Inicio.HasValue && parameters.Fim.HasValue) {
                agendas = agendas.Where(a => a.Data.Value.Date >= parameters.Inicio.Value.Date && a.Data.Value.Date <= parameters.Fim.Value.Date);
            }

            if (parameters.CodFilial > 0) {
                agendas = agendas.Where(a => a.CodFilial == parameters.CodFilial);
            }

            if (parameters.CodOS > 0) {
                agendas = agendas.Where(a => a.CodOS == parameters.CodOS);
            }

            if (parameters.IndAtivo > 0) {
                agendas = agendas.Where(a => a.IndAtivo == parameters.IndAtivo);
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodTecnicos))
            {
                int[] tecnicos = parameters.CodTecnicos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                agendas = agendas.Where(a => tecnicos.Contains(a.CodTecnico));
            }

            return agendas.ToList();
        }
        public List<AgendaTecnico> ObterPorParametros(AgendaTecnicoParameters parameters)
        {
            var agendas = _context
                            .AgendaTecnico
                                .Include(a => a.OrdemServico)
                                .AsNoTracking()
                                .AsQueryable();

            if (parameters.Inicio.HasValue && parameters.Fim.HasValue) {
                agendas = agendas.Where(a => a.Data.Value.Date >= parameters.Inicio.Value.Date && a.Data.Value.Date <= parameters.Fim.Value.Date);
            }

            if (parameters.CodOS != null) {
                agendas = agendas.Where(a => a.CodOS == parameters.CodOS);
            }

            if (parameters.IndAtivo != null) {
                agendas = agendas.Where(a => a.IndAtivo == parameters.IndAtivo);
            }

            return agendas.ToList();
        }
    }
}
