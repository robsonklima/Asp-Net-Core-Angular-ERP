using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;

namespace SAT.INFRA.Repository
{
    public partial class RelatorioAtendimentoRepository : IRelatorioAtendimentoRepository
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
            var relatorio = _context.RelatorioAtendimento
                .Include(r => r.StatusServico)
                .Include(r => r.CheckinsCheckouts)
                .Include(r => r.Tecnico)
                    .ThenInclude(r => r.Usuario)
                .Include(r => r.Tecnico)
                    .ThenInclude(t => t.Cidade)
                        .ThenInclude(t => t.UnidadeFederativa)
                            .ThenInclude(t => t.Pais)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.TipoServico)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.TipoCausa)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.Causa)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.Acao)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.Defeito)
                .Include(r => r.RelatorioAtendimentoDetalhes).ThenInclude(d => d.GrupoCausa)
                .Include(r => r.RelatorioAtendimentoDetalhes)
                    .ThenInclude(d => d.RelatorioAtendimentoDetalhePecas)
                        .ThenInclude(dp => dp.Peca);

            return relatorio.FirstOrDefault(rat => rat.CodRAT == codigo);
        }

        public PagedList<RelatorioAtendimento> ObterPorParametros(RelatorioAtendimentoParameters parameters)
        {
            var relatorios = this.ObterQuery(parameters);
            return PagedList<RelatorioAtendimento>.ToPagedList(relatorios, parameters.PageNumber, parameters.PageSize);
        }

        public IQueryable<RelatorioAtendimento> ObterQuery(RelatorioAtendimentoParameters parameters)
        {
            IQueryable<RelatorioAtendimento> relatorios = _context.RelatorioAtendimento.AsQueryable();

            relatorios = AplicarIncludes(relatorios);
            relatorios = AplicarFiltros(relatorios, parameters);

            return relatorios.AsNoTracking();
        }
    }
}