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
    public class DespesaAdiantamentoRepository : IDespesaAdiantamentoRepository
    {
        private readonly AppDbContext _context;

        public DespesaAdiantamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaAdiantamento despesa)
        {
            throw new NotImplementedException();
        }

        public void Criar(DespesaAdiantamento despesa)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int codigo)
        {
            throw new NotImplementedException();
        }

        public DespesaAdiantamento ObterPorCodigo(int codigo)
        {
            throw new NotImplementedException();
        }

        public PagedList<DespesaAdiantamento> ObterPorParametros(DespesaAdiantamentoParameters parameters)
        {
            var despesaAdiantamento = _context.DespesaAdiantamento
            .Include(da => da.DespesaAdiantamentoTipo)
            .Include(da => da.Tecnico)
            .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.CodTecnicos))
            {
                var codigos = parameters.CodTecnicos.Split(",").Select(a => a.Trim());
                despesaAdiantamento =
                    despesaAdiantamento.Where(e => codigos.Any(p => p == e.CodTecnico.ToString()));
            }

            if (parameters.IndAtivo.HasValue)
                despesaAdiantamento =
                    despesaAdiantamento.Where(e => e.IndAtivo == parameters.IndAtivo);

            if (!string.IsNullOrEmpty(parameters.CodDespesaAdiantamentoTipo))
            {
                var tipos = parameters.CodDespesaAdiantamentoTipo.Split(",").Select(a => a.Trim());
                despesaAdiantamento =
                    despesaAdiantamento.Where(e => tipos.Any(p => p == e.CodDespesaAdiantamentoTipo.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                despesaAdiantamento = despesaAdiantamento.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaAdiantamento>.ToPagedList(despesaAdiantamento, parameters.PageNumber, parameters.PageSize);
        }
    }
}