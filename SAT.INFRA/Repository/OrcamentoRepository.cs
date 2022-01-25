using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SAT.INFRA.Repository
{
    public class OrcamentoRepository : IOrcamentoRepository
    {
        private readonly AppDbContext _context;

        public OrcamentoRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Atualizar(Orcamento orcamento)
        {
            Orcamento p = _context.Orcamento.FirstOrDefault(p => p.CodOrc == orcamento.CodOrc);

            if (p != null)
            {
                _context.ChangeTracker.Clear();
                _context.Entry(p).CurrentValues.SetValues(orcamento);
                _context.SaveChanges();
            }
        }

        public void Criar(Orcamento orcamento)
        {
            _context.Add(orcamento);
            _context.SaveChanges();
        }

        public void Deletar(int codOrc)
        {
            Orcamento orcamento = _context.Orcamento.FirstOrDefault(p => p.CodOrc == codOrc);

            if (orcamento != null)
            {
                _context.Orcamento.Remove(orcamento);
                _context.SaveChanges();
            }
        }

        public Orcamento ObterPorCodigo(int codigo)
        {
            return _context.Orcamento
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.StatusServico)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.TipoIntervencao)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.Autorizada)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.Regiao)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.Cliente)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.LocalAtendimento)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.EquipamentoContrato)
                        .ThenInclude(e => e.Equipamento)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.RelatoriosAtendimento)
                        .ThenInclude(s => s.Laudos)
                            .ThenInclude(s => s.LaudoStatus)
                .Include(p => p.EnderecoFaturamentoNF)
                    .ThenInclude(p => p.CidadeEnvioNF)
                        .ThenInclude(p => p.UnidadeFederativa)
                .Include(p => p.EnderecoFaturamentoNF)
                    .ThenInclude(p => p.CidadeFaturamento)
                        .ThenInclude(p => p.UnidadeFederativa)
                .Include(p => p.OrcamentoMotivo)
                .Include(p => p.Materiais)
                    .ThenInclude(p => p.Peca)
                .Include(p => p.MaoDeObra)
                .Include(p => p.OutrosServicos)
                .Include(p => p.Descontos)
                .Include(p => p.OrcamentoStatus)
                .Include(p => p.OrcamentoDeslocamento)
                .FirstOrDefault(p => p.CodOrc == codigo);
        }

        public PagedList<Orcamento> ObterPorParametros(OrcamentoParameters parameters)
        {
            var query = _context.Orcamento
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.StatusServico)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.TipoIntervencao)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.Autorizada)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.Regiao)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.Cliente)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.LocalAtendimento)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.EquipamentoContrato)
                        .ThenInclude(e => e.Equipamento)
                .Include(o => o.OrdemServico)
                    .ThenInclude(s => s.RelatoriosAtendimento)
                        .ThenInclude(s => s.Laudos)
                            .ThenInclude(s => s.LaudoStatus)
                .AsQueryable();

            if (parameters.Filter != null)
            {
                query = query.Where(p =>
                    p.CodOrc.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (!string.IsNullOrEmpty(parameters.codStatusServicos))
            {
                var statusServicos = parameters.codStatusServicos.Split(',').Select(a => a.Trim()).ToArray();
                query = query.Where(orc => statusServicos.Any(f => f == orc.OrdemServico.CodStatusServico.ToString()));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<Orcamento>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
