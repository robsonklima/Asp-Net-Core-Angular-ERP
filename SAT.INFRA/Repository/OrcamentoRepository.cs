using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
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
            _context.ChangeTracker.Clear();
            Orcamento p = _context.Orcamento.FirstOrDefault(p => p.CodOrc == orcamento.CodOrc);

            if (p != null)
            {
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
            Orcamento orcamento = _context
                                    .Orcamento
                                        .Include(p => p.OrcamentoMotivo)
                                        .Include(p => p.Materiais)
                                        .Include(p => p.MaoDeObra)
                                        .Include(p => p.OutrosServicos)
                                        .Include(p => p.Descontos)
                                        .Include(p => p.OrcamentoStatus)
                                        .Include(p => p.OrcamentoDeslocamento)
                                        .FirstOrDefault(p => p.CodOrc == codOrc);

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
                .Include(p => p.LocalEnvioNFFaturamento)
                    .ThenInclude(p => p.CidadeEnvioNF)
                        .ThenInclude(p => p.UnidadeFederativa)
                .Include(p => p.LocalEnvioNFFaturamento)
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

            if (!string.IsNullOrEmpty(parameters.codClientes))
            {
                var clientes = parameters.codClientes.Split(',').Select(a => a.Trim()).ToArray();
                query = query.Where(orc => clientes.Any(f => f == orc.OrdemServico.CodCliente.ToString()));
            }    

            if (!string.IsNullOrEmpty(parameters.codFiliais))
            {
                var filiais = parameters.codFiliais.Split(',').Select(a => a.Trim()).ToArray();
                query = query.Where(orc => filiais.Any(f => f == orc.OrdemServico.CodFilial.ToString()));
            }                       

            if (!string.IsNullOrWhiteSpace(parameters.CodTiposIntervencao))
            {
                int[] tiposIntervencao = parameters.CodTiposIntervencao.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(orc => tiposIntervencao.Contains(orc.OrdemServico.TipoIntervencao.CodTipoIntervencao.Value));
            }            

            if (parameters.CodEquipContrato.HasValue)
                query = query.Where(orc => orc.OrdemServico.EquipamentoContrato.CodEquipContrato == parameters.CodEquipContrato);

            if (!string.IsNullOrWhiteSpace(parameters.NumOSCliente))
                query = query.Where(orc => orc.OrdemServico.NumOSCliente == parameters.NumOSCliente);

            if (parameters.CodigoOrdemServico.HasValue)
               query = query.Where(orc => orc.CodigoOrdemServico == parameters.CodigoOrdemServico);

            if (!string.IsNullOrWhiteSpace(parameters.NumOSQuarteirizada))
                query = query.Where(orc => orc.OrdemServico.NumOSQuarteirizada == parameters.NumOSQuarteirizada);

            if (!string.IsNullOrWhiteSpace(parameters.NumSerie))
                query = query.Where(orc => orc.OrdemServico.EquipamentoContrato.NumSerie.Trim().ToLower() == parameters.NumSerie.Trim().ToLower());                

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy($"{parameters.SortActive} {parameters.SortDirection}");
            }

            return PagedList<Orcamento>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }
    }
}
