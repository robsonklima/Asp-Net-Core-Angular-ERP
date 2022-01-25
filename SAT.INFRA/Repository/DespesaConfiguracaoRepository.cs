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
    public class DespesaConfiguracaoRepository : IDespesaConfiguracaoRepository
    {
        private readonly AppDbContext _context;

        public DespesaConfiguracaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(DespesaConfiguracao config)
        {
            DespesaConfiguracao d =
             _context.DespesaConfiguracao
             .FirstOrDefault(l => l.CodDespesaConfiguracao == config.CodDespesaConfiguracao);

            if (d != null)
            {
                try
                {
                    _context.ChangeTracker.Clear();
                    _context.Entry(d).CurrentValues.SetValues(config);
                    _context.SaveChanges();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public void Criar(DespesaConfiguracao config)
        {
            _context.Add(config);
            _context.SaveChanges();
        }

        public DespesaConfiguracao ObterPorCodigo(int codigo) =>
            _context.DespesaConfiguracao
                .AsQueryable()
                .FirstOrDefault(i => i.CodDespesaConfiguracao == codigo);

        public PagedList<DespesaConfiguracao> ObterPorParametros(DespesaConfiguracaoParameters parameters)
        {
            var configuracoes =
                _context.DespesaConfiguracao.AsQueryable();

            if (parameters.IndAtivo.HasValue)
                configuracoes = configuracoes.Where(i => i.IndAtivo == parameters.IndAtivo);

            if (!string.IsNullOrEmpty(parameters.SortActive) && !string.IsNullOrEmpty(parameters.SortDirection))
                configuracoes = configuracoes.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));

            return PagedList<DespesaConfiguracao>.ToPagedList(configuracoes, parameters.PageNumber, parameters.PageSize);
        }
    }
}