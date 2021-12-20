using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<AgendaTecnico> AtualizarAsync(AgendaTecnico agenda)
        {
            return await Task.Run(() =>
            {
                try
                {
                    AgendaTecnico a = _context.AgendaTecnico.FirstOrDefault(a => a.CodAgendaTecnico == agenda.CodAgendaTecnico);

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
            });
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
            var agendas = _context.AgendaTecnico
            .Include(i => i.OrdemServico)
                .ThenInclude(i => i.TipoIntervencao)
            .Include(i => i.OrdemServico)
                .ThenInclude(i => i.LocalAtendimento)
            .Include(i => i.OrdemServico)
                .ThenInclude(i => i.StatusServico)
            .Include(i => i.OrdemServico)
                .ThenInclude(i => i.Tecnico)
            .Include(i => i.OrdemServico)
                .ThenInclude(i => i.PrazosAtendimento)
            .AsQueryable();

            if (parameters.InicioPeriodoAgenda.HasValue && parameters.FimPeriodoAgenda.HasValue)
                agendas = agendas.Where(ag => ag.Inicio.Date >= parameters.InicioPeriodoAgenda.Value.Date && ag.Fim.Date <= parameters.FimPeriodoAgenda.Value.Date);

            if (parameters.Tipo.HasValue)
                agendas = agendas.Where(ag => ag.Tipo == parameters.Tipo);

            if (parameters.CodTecnico.HasValue)
                agendas = agendas.Where(ag => ag.CodTecnico == parameters.CodTecnico);

            if (!string.IsNullOrEmpty(parameters.CodTecnicos))
            {
                var tecnicos = parameters.CodTecnicos.Split(",").Select(t => t.Trim());
                agendas = agendas.Where(ag => tecnicos.Any(r => r == ag.CodTecnico.ToString()));
            }

            if (parameters.CodOS.HasValue)
                agendas = agendas.Where(ag => ag.CodOS == parameters.CodOS);

            if (parameters.IndAtivo.HasValue)
                agendas = agendas.Where(ag => ag.IndAtivo == parameters.IndAtivo);

            return agendas;
        }
    }
}
