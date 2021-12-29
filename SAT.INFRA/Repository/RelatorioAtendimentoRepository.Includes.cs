using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class RelatorioAtendimentoRepository : IRelatorioAtendimentoRepository
    {
        public IQueryable<RelatorioAtendimento> AplicarIncludes(IQueryable<RelatorioAtendimento> query) =>
            _context.RelatorioAtendimento
                .Include(r => r.Tecnico)
                .Include(r => r.StatusServico)
                .Include(r => r.CheckinsCheckouts)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.TipoServico)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.TipoCausa)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.GrupoCausa)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.Causa)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.Acao)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.Defeito)
                .Include(r => r.StatusServico)
                .AsQueryable();
    }
}