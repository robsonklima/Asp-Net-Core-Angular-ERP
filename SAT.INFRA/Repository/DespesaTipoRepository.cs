using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public class DespesaTipoRepository : IDespesaTipoRepository
    {
        private readonly AppDbContext _context;

        public DespesaTipoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaTipo despesaTipo)
        {
            throw new NotImplementedException();
        }

        public void Criar(DespesaTipo despesaTipo)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaTipo ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DespesaTipo> ObterPorParametros(DespesaTipoParameters parameters)
        {
            var despesaTipos = _context.DespesaTipo.AsQueryable();

            if (parameters.IndAtivo.HasValue)
                despesaTipos = despesaTipos.Where(e => e.IndAtivo == parameters.IndAtivo);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesaTipos = despesaTipos.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaTipo>.ToPagedList(despesaTipos, parameters.PageNumber, parameters.PageSize);
        }
    }
}