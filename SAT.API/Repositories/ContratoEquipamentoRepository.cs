using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;

namespace SAT.API.Repositories
{
    public class ContratoEquipamentoRepository : IContratoEquipamentoRepository
    {
        private readonly AppDbContext _context;

        public ContratoEquipamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<ContratoEquipamento> ObterPorParametros(ContratoEquipamentoParameters parameters)
        {
            var contratoEquipamentos = _context.ContratoEquipamento
                .Include(c => c.Contrato)
                .Include(c => c.TipoEquipamento)
                .Include(c => c.GrupoEquipamento)
                .Include(c => c.Equipamento)
                .AsQueryable();

            if (parameters.CodContrato != null)
            {
                contratoEquipamentos = contratoEquipamentos.Where(a => a.CodContrato == parameters.CodContrato);
            }

            if (parameters.CodTipoEquip != null)
            {
                contratoEquipamentos = contratoEquipamentos.Where(a => a.CodTipoEquip == parameters.CodTipoEquip);
            }

            if (parameters.CodGrupoEquip != null)
            {
                contratoEquipamentos = contratoEquipamentos.Where(a => a.CodGrupoEquip == parameters.CodGrupoEquip);
            }
            
            if (parameters.CodEquip != null)
            {
                contratoEquipamentos = contratoEquipamentos.Where(a => a.CodGrupoEquip == parameters.CodGrupoEquip);
            }

            return PagedList<ContratoEquipamento>.ToPagedList(contratoEquipamentos, parameters.PageNumber, parameters.PageSize);
        }
    }
}
