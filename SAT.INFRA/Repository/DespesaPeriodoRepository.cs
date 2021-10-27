using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class DespesaPeriodoRepository : IDespesaPeriodoRepository
    {
        private readonly AppDbContext _context;

        public DespesaPeriodoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaPeriodo despesa)
        {
            throw new NotImplementedException();
        }

        public void Criar(DespesaPeriodo despesa)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaPeriodo ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DespesaPeriodo> ObterPorParametros(DespesaPeriodoParameters parameters)
        {
            var despesasPeriodo = _context.DespesaPeriodo.AsQueryable();

            if (parameters.IndAtivo.HasValue)
                despesasPeriodo = despesasPeriodo.Where(e => e.IndAtivo == parameters.IndAtivo);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesasPeriodo = despesasPeriodo.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaPeriodo>.ToPagedList(despesasPeriodo, parameters.PageNumber, parameters.PageSize);
        }
    }
}