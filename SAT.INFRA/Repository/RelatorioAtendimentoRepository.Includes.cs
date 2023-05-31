using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Enums;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class RelatorioAtendimentoRepository : IRelatorioAtendimentoRepository
    {
        public IQueryable<RelatorioAtendimento> AplicarIncludes(IQueryable<RelatorioAtendimento> query, RelatorioAtendimentoIncludeEnum include)
        {
            switch (include)
            {
                case RelatorioAtendimentoIncludeEnum.RAT_OS:
                    query = query
                         .Include(r => r.Tecnico)
                         .Include(r => r.Fotos)
                         .Include(r => r.ProtocolosSTN);
                    break;
                default:
                    query = query
                        .Include(r => r.Tecnico)
                        .Include(r => r.Fotos)
                        .Include(r => r.StatusServico)
                        .Include(r => r.CheckinsCheckouts)
                        .Include(r => r.ProtocolosSTN)
                        .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.TipoServico)
                        .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.TipoCausa)
                        .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.GrupoCausa)
                        .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.Causa)
                        .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.Acao)
                        .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.Defeito)
                        .Include(r => r.StatusServico)
                        .Include(rat => rat.RelatorioAtendimentoDetalhes)
                            .ThenInclude(rat => rat.RelatorioAtendimentoDetalhePecas)
                                .ThenInclude(rat => rat.Peca)
                        .Include(a => a.TipoServico)
                        .Include(a => a.Laudos)
                            .ThenInclude(a => a.LaudosSituacao)
                        .Include(a => a.Laudos)
                            .ThenInclude(a => a.LaudoStatus);
                    break;
            }

            return query;
        }
    }
}