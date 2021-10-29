using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
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
            throw new NotImplementedException();
        }

        public void Criar(DespesaAdiantamentoPeriodo despesa)
        {
            throw new NotImplementedException();
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
            .Include(dap => dap.DespesaPeriodo)
            .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.CodDespesaPeriodos))
            {
                var periodos = parameters.CodDespesaPeriodos.Split(',').Select(f => f.Trim());
                despesaAdiantamentoPeriodo = despesaAdiantamentoPeriodo.Where(e => periodos.Any(p => p == e.CodDespesaPeriodo.ToString()));
            }

            if (parameters.CodTecnico.HasValue)
                despesaAdiantamentoPeriodo = despesaAdiantamentoPeriodo.Where(e => e.DespesaAdiantamento.CodTecnico == parameters.CodTecnico);

            if (parameters.IndAtivoPeriodo.HasValue)
                despesaAdiantamentoPeriodo = despesaAdiantamentoPeriodo.Where(e => e.DespesaPeriodo.IndAtivo == parameters.IndAtivoPeriodo);

            return PagedList<DespesaAdiantamentoPeriodo>.ToPagedList(despesaAdiantamentoPeriodo, parameters.PageNumber, parameters.PageSize);
        }
    }
}