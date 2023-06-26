using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class OrdemServicoHistoricoRepository : IOrdemServicoHistoricoRepository
    {
        private readonly AppDbContext _context;

        public OrdemServicoHistoricoRepository(AppDbContext context)
        {
            _context = context;
        }

        public PagedList<OrdemServicoHistorico> ObterPorParametros(OrdemServicoHistoricoParameters parameters)
        {
            var hist = _context.OrdemServicoHistorico
                .Include(h => h.Usuario)
                .Include(h => h.Autorizada)
                .Include(h => h.Tecnico)
                .Include(h => h.Cliente)
                .Include(h => h.LocalAtendimento)
                .Include(h => h.EquipamentoContrato)
                .Include(h => h.TipoIntervencao)
                .Include(h => h.StatusServico)
                .AsQueryable();

            if (parameters.CodOS != null)
            {
                hist = hist.Where(n => n.CodOS == parameters.CodOS);
            }

            if (parameters.DataHoraCadInicio.HasValue && parameters.DataHoraCadFim.HasValue)
            {
                hist = hist.Where(os => os.DataHoraCad.Date >= parameters.DataHoraCadInicio.Value.Date
                    && os.DataHoraCad.Date <= parameters.DataHoraCadFim.Value.Date);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                hist = hist.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<OrdemServicoHistorico>.ToPagedList(hist, parameters.PageNumber, parameters.PageSize);
        }
    }
}
