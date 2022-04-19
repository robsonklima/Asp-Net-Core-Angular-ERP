using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Helpers;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace SAT.INFRA.Repository
{
    public class DespesaCartaoCombustivelRepository : IDespesaCartaoCombustivelRepository
    {
        private readonly AppDbContext _context;

        public DespesaCartaoCombustivelRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaCartaoCombustivel cartao)
        {
            _context.ChangeTracker.Clear();
            DespesaCartaoCombustivel d = _context.DespesaCartaoCombustivel.FirstOrDefault(l => l.CodDespesaCartaoCombustivel == cartao.CodDespesaCartaoCombustivel);

            if (d != null)
            {
                try
                {
                    _context.Entry(d).CurrentValues.SetValues(cartao);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"", ex);
                }
            }
        }

        public void Criar(DespesaCartaoCombustivel cartao)
        {
            _context.Add(cartao);
            _context.SaveChanges();
        }

        public DespesaCartaoCombustivel ObterPorCodigo(int codigo) =>
            _context.DespesaCartaoCombustivel
                .AsQueryable()
                .FirstOrDefault(i => i.CodDespesaCartaoCombustivel == codigo);
        public PagedList<DespesaCartaoCombustivel> ObterPorParametros(DespesaCartaoCombustivelParameters parameters)
        {
            var cartoes = _context.DespesaCartaoCombustivel.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Filter))
                cartoes = cartoes.Where(c =>
                c.Numero.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty));

            if (parameters.CodDespesaCartaoCombustivel.HasValue)
                cartoes = cartoes.Where(a => a.CodDespesaCartaoCombustivel == parameters.CodDespesaCartaoCombustivel);

            if (parameters.IndAtivo.HasValue)
                cartoes = cartoes.Where(a => a.IndAtivo == parameters.IndAtivo);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                cartoes = cartoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaCartaoCombustivel>.ToPagedList(cartoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}
