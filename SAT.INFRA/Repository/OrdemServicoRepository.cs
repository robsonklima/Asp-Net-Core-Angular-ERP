using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
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
            OrdemServico os = _context.OrdemServico.FirstOrDefault(os => os.CodOS == ordemServico.CodOS);

            if (os != null)
            {
                _context.Entry(os).CurrentValues.SetValues(ordemServico);
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

        public OrdemServico ObterPorCodigo(int codigo)
        {
            var ordemServico = _context.OrdemServico
                .Include(os => os.StatusServico)
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
                .Include(os => os.EquipamentoContrato.Contrato)
                .Include(os => os.EquipamentoContrato.AcordoNivelServico)
                .Include(os => os.RegiaoAutorizada)
                .Include(os => os.RegiaoAutorizada.Filial)
                .Include(os => os.RegiaoAutorizada.Autorizada)
                .Include(os => os.RegiaoAutorizada.Regiao)
                .Include(os => os.Cliente)
                .Include(os => os.Cliente.Cidade)
                .Include(os => os.Tecnico)
                .Include(os => os.RelatoriosAtendimento)
                .Include(os => os.Fotos)
                .Include(os => os.Agendamentos)
                .Include(os => os.PrazosAtendimento)
                .Include(os => os.Intencoes)
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
                    .ThenInclude(a => a.CheckinsCheckouts)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.Laudos)
                        .ThenInclude(a => a.LaudosSituacao)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.Laudos)
                        .ThenInclude(a => a.LaudoStatus)
                .Include(os => os.OrdensServicoRelatorioInstalacao)
                .FirstOrDefault(os => os.CodOS == codigo);

            return ordemServico;
        }
    }
}