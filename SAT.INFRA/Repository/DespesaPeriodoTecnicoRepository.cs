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

            if (parameters.CodDespesaPeriodo.HasValue)
                despesasPeriodoTecnico =
                    despesasPeriodoTecnico.Where(e => e.CodDespesaPeriodo == parameters.CodDespesaPeriodo);

            if (parameters.CodTecnico.HasValue)
                despesasPeriodoTecnico =
                    despesasPeriodoTecnico.Where(e => e.CodTecnico == parameters.CodTecnico);

            if (parameters.IndAtivoPeriodo.HasValue)
                despesasPeriodoTecnico =
                    despesasPeriodoTecnico.Where(e => e.DespesaPeriodo.IndAtivo == parameters.IndAtivoPeriodo);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesasPeriodoTecnico =
                    despesasPeriodoTecnico.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaPeriodoTecnico>.ToPagedList(despesasPeriodoTecnico, parameters.PageNumber, parameters.PageSize);
        }
    }
}