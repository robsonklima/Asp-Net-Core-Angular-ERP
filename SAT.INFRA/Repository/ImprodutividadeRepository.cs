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
    public class ImprodutividadeRepository : IImprodutividadeRepository
    {
        private readonly AppDbContext _context;

        public ImprodutividadeRepository(AppDbContext context)
        {
            _context = context;
        }

        public Improdutividade ObterPorCodigo(int codImprodutividade)
        {
            try
            {
                return _context.Improdutividade
                    .SingleOrDefault(p => p.CodImprodutividade == codImprodutividade);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao consultar a Improdutividade {ex.Message}");
            }
        }

        public PagedList<Improdutividade> ObterPorParametros(ImprodutividadeParameters parameters)
        {
            var improdutividades = _context.Improdutividade
                .AsQueryable();

            if (parameters.CodImprodutividade.HasValue)
            {
                improdutividades = improdutividades.Where(p => p.CodImprodutividade == parameters.CodImprodutividade);
            }

            if (parameters.IndAtivo.HasValue)
            {
                improdutividades = improdutividades.Where(p => p.IndAtivo == parameters.IndAtivo);
            }

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                improdutividades = improdutividades.Where(a =>
                    a.CodImprodutividade.ToString().Contains(parameters.Filter));
            }

            
            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                improdutividades = improdutividades.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Improdutividade>.ToPagedList(improdutividades, parameters.PageNumber, parameters.PageSize);
        }

    }
}
