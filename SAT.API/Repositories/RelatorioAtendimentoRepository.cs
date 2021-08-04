using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System.Reflection;

namespace SAT.API.Repositories
{
    public class RelatorioAtendimentoRepository : IRelatorioAtendimentoRepository
    {
        private readonly AppDbContext _context;

        public RelatorioAtendimentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(RelatorioAtendimento relatorioAtendimento)
        {
            RelatorioAtendimento rat = _context.RelatorioAtendimento.FirstOrDefault(rat => rat.CodRAT == relatorioAtendimento.CodRAT);

            if (rat != null)
            {
                _context.Entry(rat).CurrentValues.SetValues(relatorioAtendimento);
                _context.SaveChanges();
            }
        }

        public void Criar(RelatorioAtendimento relatorioAtendimento)
        {
            _context.Add(relatorioAtendimento);
            _context.SaveChanges();
        }

        public void Deletar(int codRAT)
        {
            RelatorioAtendimento rat = _context.RelatorioAtendimento.FirstOrDefault(rat => rat.CodRAT == codRAT);

            if (rat != null)
            {
                _context.RelatorioAtendimento.Remove(rat);
                _context.SaveChanges();
            }
        }

        public RelatorioAtendimento ObterPorCodigo(int codigo)
        {
            return _context.RelatorioAtendimento
                .Include(r => r.RelatorioAtendimentoDetalhes)
                    .ThenInclude(r => r.RelatorioAtendimentoDetalhePecas)
                .FirstOrDefault(rat => rat.CodRAT == codigo);
        }

        public PagedList<RelatorioAtendimento> ObterPorParametros(RelatorioAtendimentoParameters parameters)
        {
            var relatorios = _context.RelatorioAtendimento
                .Include(r => r.Tecnico)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(t => t.TipoCausa)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(g => g.GrupoCausa)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(c => c.Causa)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(a => a.Acao)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.Defeito)
                .Include(r => r.StatusServico)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                relatorios = relatorios.Where(
                    r =>
                    r.CodRAT.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    r.NumRAT.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodRAT != null)
            {
                relatorios = relatorios.Where(r => r.CodRAT == parameters.CodRAT);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                relatorios = relatorios.OrderBy(parameters.SortActive, parameters.SortDirection);
            }

            return PagedList<RelatorioAtendimento>.ToPagedList(relatorios, parameters.PageNumber, parameters.PageSize);
        }
    }
}
