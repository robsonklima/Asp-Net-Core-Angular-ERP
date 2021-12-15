using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class EquipamentoRepository : IEquipamentoRepository
    {
        public IQueryable<Equipamento> AplicarFiltroChamados(IQueryable<Equipamento> equips, EquipamentoParameters parameters)
        {
            var equipContrato = _context.EquipamentoContrato
                // .Where(i => i.IndAtivo == 1)
                .AsQueryable();

            if (!string.IsNullOrEmpty(parameters.CodClientes))
            {
                int[] clientes = parameters.CodClientes.Split(",").Select(c => int.Parse(c.Trim())).Distinct().ToArray();
                equipContrato = equipContrato.Where(e => clientes.Contains(e.CodCliente));
            }

            var codEquips = equipContrato
                .Select(i => i.CodEquip)
                .Distinct();

            equips = equips
                .Where(i => codEquips.Any(c => c == i.CodEquip))
                .OrderBy(i => i.NomeEquip);

            return equips;
        }
    }
}
