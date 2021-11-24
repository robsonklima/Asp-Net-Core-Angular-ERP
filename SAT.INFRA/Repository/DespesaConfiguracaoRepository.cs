using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
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