using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using System;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        private readonly AppDbContext _context;
        private readonly IFeriadoRepository _feriadoRepository;

        public OrdemServicoRepository(AppDbContext context, IFeriadoRepository feriadoRepository)
        {
            _context = context;
            _feriadoRepository = feriadoRepository;
        }

        private int CalculaDiasNaoUteis(DateTime dataInicio, DateTime dataFim, bool contabilizarSabado = false, bool contabilizarDomingo = false, bool contabilizarFeriados = false, int? codCidade = null)
        {
            return this._feriadoRepository.CalculaDiasNaoUteis(dataInicio, dataFim, contabilizarSabado, contabilizarDomingo, contabilizarFeriados, codCidade);
        }

        public void Criar(OrdemServico ordemServico)
        {
            _context.Add(ordemServico);
            _context.SaveChanges();
        }

        public void Atualizar(OrdemServico ordemServico)
        {
            _context.ChangeTracker.Clear();
            OrdemServico os = _context.OrdemServico.FirstOrDefault(os => os.CodOS == ordemServico.CodOS);

            if (os != null)
            {
                _context.Entry(os).CurrentValues.SetValues(ordemServico);
                _context.Entry(os).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        public void Deletar(int codOS)
        {
            OrdemServico os = _context.OrdemServico.FirstOrDefault(os => os.CodOS == codOS);

            if (os != null)
            {
                _context.OrdemServico.Remove(os);
                _context.SaveChanges();
            }
        }

        public PagedList<OrdemServico> ObterPorParametros(OrdemServicoParameters parameters)
        {
            IQueryable<OrdemServico> query = this.ObterQuery(parameters);

            return PagedList<OrdemServico>
                .ToPagedList(query, parameters.PageNumber, parameters.PageSize,
                    new OrdemServicoComparer());
        }

        public IQueryable<OrdemServico> ObterQuery(OrdemServicoParameters parameters)
        {
            IQueryable<OrdemServico> query = _context.OrdemServico.AsQueryable();

            query = AplicarIncludes(query, parameters.Include);
            query = AplicarFiltros(query, parameters);
            query = AplicarOrdenacao(query, parameters.SortActive, parameters.SortDirection);

            return query.AsNoTracking();
        }

        public OrdemServico ObterPorCodigo(int codigo) =>
            _context.OrdemServico
                .Include(os => os.StatusServico)
                .Include(os => os.Filial)
                .Include(os => os.UsuarioCadastro)
                .Include(os => os.UsuarioCad)
                .Include(os => os.Autorizada)
                .Include(os => os.Regiao)
                .Include(os => os.TipoIntervencao)
                .Include(os => os.LocalAtendimento)
                .Include(os => os.LocalAtendimento.Cidade)
                .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
                    .ThenInclude(uf => uf.DispBBRegiaoUF)
                        .ThenInclude(uf => uf.DispBBRegiao)
                .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa.Pais)
                .Include(os => os.Equipamento)
                .Include(os => os.EquipamentoContrato)
                    .ThenInclude(ec => ec.DispBBCriticidade)
                .Include(os => os.EquipamentoContrato)
                    .ThenInclude(os => os.Equipamento)
                .Include(os => os.EquipamentoContrato)
                    .ThenInclude(ec => ec.Filial)
                .Include(os => os.EquipamentoContrato)
                    .ThenInclude(ec => ec.Autorizada)
                 .Include(os => os.EquipamentoContrato)
                    .ThenInclude(ec => ec.Regiao)
                .Include(os => os.EquipamentoContrato)
                    .ThenInclude(ec => ec.Contrato)
                .Include(os => os.EquipamentoContrato)
                    .ThenInclude(ec => ec.AcordoNivelServico)
                .Include(os => os.RegiaoAutorizada)
                .Include(os => os.RegiaoAutorizada.Filial)
                .Include(os => os.RegiaoAutorizada.Autorizada)
                .Include(os => os.RegiaoAutorizada.Regiao)
                .Include(os => os.Cliente)
                .Include(os => os.Cliente.Cidade)
                .Include(os => os.Tecnico)
                .Include(os => os.RelatoriosAtendimento)
                .Include(os => os.PrazosAtendimento)
                .Include(os => os.Intencoes)
                .Include(os => os.Agendamentos)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.ProtocolosSTN)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.RelatorioAtendimentoDetalhes)
                        .ThenInclude(d => d.TipoCausa)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.RelatorioAtendimentoDetalhes)
                        .ThenInclude(d => d.GrupoCausa)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.RelatorioAtendimentoDetalhes)
                        .ThenInclude(d => d.Causa)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.RelatorioAtendimentoDetalhes)
                        .ThenInclude(d => d.Acao)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.RelatorioAtendimentoDetalhes)
                        .ThenInclude(d => d.Defeito)
                .Include(os => os.RelatoriosAtendimento)
                        .ThenInclude(rat => rat.RelatorioAtendimentoDetalhes)
                            .ThenInclude(ratd => ratd.TipoServico)
                 .Include(os => os.RelatoriosAtendimento)
                        .ThenInclude(rat => rat.RelatorioAtendimentoDetalhes)
                            .ThenInclude(rat => rat.RelatorioAtendimentoDetalhePecas)
                                .ThenInclude(rat => rat.Peca)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.Tecnico)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.StatusServico)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.TipoServico)
                .Include(os => os.RelatoriosAtendimento)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.Laudos)
                        .ThenInclude(a => a.LaudosSituacao)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.Laudos)
                        .ThenInclude(a => a.LaudoStatus)
                .Include(os => os.OrdensServicoRelatorioInstalacao)
                .Include(os => os.Orcamentos)
                    .ThenInclude(orc => orc.OrcamentoMotivo)
                .AsNoTracking()
                .FirstOrDefault(os => os.CodOS == codigo);
    }
}