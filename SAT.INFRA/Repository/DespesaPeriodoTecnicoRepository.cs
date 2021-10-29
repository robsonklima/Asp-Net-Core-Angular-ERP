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
    public class DespesaPeriodoTecnicoRepository : IDespesaPeriodoTecnicoRepository
    {
        private readonly AppDbContext _context;

        public DespesaPeriodoTecnicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaPeriodoTecnico despesaTecnico)
        {
            throw new NotImplementedException();
        }

        public void Criar(DespesaPeriodoTecnico despesa)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaPeriodoTecnico ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DespesaPeriodoTecnico> ObterPorParametros(DespesaPeriodoTecnicoParameters parameters)
        {
            var despesasPeriodoTecnico = _context.DespesaPeriodoTecnico
                .Include(dpt => dpt.DespesaPeriodo)
                .Include(dpt => dpt.Despesas)
                    .ThenInclude(dp => dp.DespesaItens)
                .Include(dpt => dpt.DespesaPeriodoTecnicoStatus)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.CodDespesaPeriodos))
            {
                var periodos = parameters.CodDespesaPeriodos.Split(',').Select(f => f.Trim());
                despesasPeriodoTecnico = despesasPeriodoTecnico.Where(e => periodos.Any(p => p == e.CodDespesaPeriodo.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodTecnicos))
            {
                var tecnicos = parameters.CodTecnicos.Split(',').Select(f => f.Trim());
                despesasPeriodoTecnico = despesasPeriodoTecnico.Where(e => tecnicos.Any(p => p == e.CodTecnico.ToString()));
            }

            if (parameters.IndAtivoPeriodo.HasValue)
                despesasPeriodoTecnico = despesasPeriodoTecnico.Where(e => e.DespesaPeriodo.IndAtivo == parameters.IndAtivoPeriodo);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesasPeriodoTecnico = despesasPeriodoTecnico.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            var t = despesasPeriodoTecnico.ToQueryString();

            return PagedList<DespesaPeriodoTecnico>.ToPagedList(despesasPeriodoTecnico, parameters.PageNumber, parameters.PageSize);
        }
    }
}