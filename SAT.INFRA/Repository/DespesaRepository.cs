using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class DespesaRepository : IDespesaRepository
    {
        private readonly AppDbContext _context;

        public DespesaRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Despesa despesa)
        {
            throw new NotImplementedException();
        }

        public Despesa Criar(Despesa despesa)
        {
            _context.Add(despesa);
            _context.SaveChanges();
            return despesa;
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public Despesa ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<Despesa> ObterPorParametros(DespesaParameters parameters)
        {
            var despesas = _context.Despesa
            .Include(d => d.DespesaItens)
                .ThenInclude(di => di.DespesaTipo)
            .Include(d => d.RelatorioAtendimento)
            .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.CodTecnico))
            {
                var codigos = parameters.CodTecnico.Split(",").Select(a => a.Trim()).Distinct();
                despesas = despesas.Where(d => codigos.Any(p => p == d.CodTecnico.ToString()));
            }

            if (parameters.DataHoraInicioRAT.HasValue)
                despesas = despesas.Where(e => e.RelatorioAtendimento != null &&
                    e.RelatorioAtendimento.DataHoraInicio >= parameters.DataHoraInicioRAT);

            if (parameters.CodDespesaPeriodo.HasValue)
                despesas = despesas.Where(e => e.CodDespesaPeriodo == parameters.CodDespesaPeriodo);

            if (!string.IsNullOrEmpty(parameters.CodRATs))
            {
                var codigos = parameters.CodRATs.Split(",").Select(a => a.Trim());
                despesas = despesas.Where(d => codigos.Any(p => p == d.CodRAT.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesas = despesas.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<Despesa>.ToPagedList(despesas, parameters.PageNumber, parameters.PageSize);
        }
    }
}
