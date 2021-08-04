using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.API.Repositories
{
    public class EquipamentoRepository : IEquipamentoRepository
    {
        private readonly AppDbContext _context;
        private readonly ILoggerRepository _logger;

        public EquipamentoRepository(AppDbContext context, ILoggerRepository logger)
        {
            _context = context;
            _logger = logger;
        }

        public Equipamento ObterPorCodigo(int codigo)
        {
            return _context.Equipamento.SingleOrDefault(e => e.CodEquip == codigo);
        }

        public PagedList<Equipamento> ObterPorParametros(EquipamentoParameters parameters)
        {
            var equips = _context.Equipamento
                .Include(e => e.TipoEquipamento)
                .Include(e => e.GrupoEquipamento)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                equips = equips.Where(
                    e =>
                    e.CodEquip.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    e.CodEEquip.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    e.NomeEquip.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodEquip != null)
            {
                equips = equips.Where(e => e.CodEquip == parameters.CodEquip);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                equips = equips.OrderBy(parameters.SortActive, parameters.SortDirection);
            }

            return PagedList<Equipamento>.ToPagedList(equips, parameters.PageNumber, parameters.PageSize);
        }
    }
}
