using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using SAT.MODELS.Entities.Params;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class DespesaAdiantamentoPeriodoRepository : IDespesaAdiantamentoPeriodoRepository
    {
        private readonly AppDbContext _context;

        public DespesaAdiantamentoPeriodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaAdiantamentoPeriodo despesa)
        {
            _context.ChangeTracker.Clear();
            DespesaAdiantamentoPeriodo d = _context.DespesaAdiantamentoPeriodo.FirstOrDefault(l => l.CodDespesaAdiantamentoPeriodo == despesa.CodDespesaAdiantamentoPeriodo);

            if (d != null)
            {
                _context.Entry(d).CurrentValues.SetValues(despesa);

                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }

        public void Criar(DespesaAdiantamentoPeriodo despesa)
        {
            try
            {
                _context.Add(despesa);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"", ex);
            }
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaAdiantamentoPeriodo ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DespesaAdiantamentoPeriodo> ObterPorParametros(DespesaAdiantamentoPeriodoParameters parameters)
        {
            var despesaAdiantamentoPeriodo = _context.DespesaAdiantamentoPeriodo
            .Include(dap => dap.DespesaAdiantamento)
                .ThenInclude(dapt => dapt.DespesaAdiantamentoTipo)
            .Include(dap => dap.DespesaPeriodo)
            .AsQueryable();

            if (parameters.CodDespesaPeriodo.HasValue)
                despesaAdiantamentoPeriodo =
                    despesaAdiantamentoPeriodo.Where(e => e.CodDespesaPeriodo == parameters.CodDespesaPeriodo);

            if (parameters.CodTecnico.HasValue)
                despesaAdiantamentoPeriodo =
                    despesaAdiantamentoPeriodo.Where(e => e.DespesaAdiantamento.CodTecnico == parameters.CodTecnico);

            if (parameters.IndAdiantamentoAtivo.HasValue)
                despesaAdiantamentoPeriodo =
                    despesaAdiantamentoPeriodo.Where(e => e.DespesaAdiantamento.IndAtivo == parameters.IndAdiantamentoAtivo);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesaAdiantamentoPeriodo = despesaAdiantamentoPeriodo.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");

            return PagedList<DespesaAdiantamentoPeriodo>.ToPagedList(despesaAdiantamentoPeriodo, parameters.PageNumber, parameters.PageSize);
        }
    }
}