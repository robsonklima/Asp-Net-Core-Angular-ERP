using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repositories
{
    public class EquipamentoRepository : IEquipamentoRepository
    {
        private readonly AppDbContext _context;

        public EquipamentoRepository(AppDbContext context)
        {
            _context = context;
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
                equips = equips.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Equipamento>.ToPagedList(equips, parameters.PageNumber, parameters.PageSize);
        }
    }
}
