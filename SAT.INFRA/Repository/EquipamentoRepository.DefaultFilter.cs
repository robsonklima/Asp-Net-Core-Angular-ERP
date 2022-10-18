using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class EquipamentoRepository : IEquipamentoRepository
    {
        public IQueryable<Equipamento> AplicarFiltroPadrao(IQueryable<Equipamento> equips, EquipamentoParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Filter))
                equips = equips.Where(e =>
                    e.CodEquip.ToString().Contains(parameters.Filter) ||
                    e.CodEEquip.Contains(parameters.Filter) ||
                    e.GrupoEquipamento.NomeGrupoEquip.Contains(parameters.Filter) ||
                    e.TipoEquipamento.NomeTipoEquip.Contains(parameters.Filter) ||
                    e.NomeEquip.Contains(parameters.Filter));

            if (parameters.CodEquip.HasValue)
                equips = equips.Where(e => e.CodEquip == parameters.CodEquip);

            return equips;
        }
    }
}
