using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq;
using System;
using System.Collections.Generic;
using SAT.MODELS.Views;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        private readonly AppDbContext _context;
        
        public OrdemServicoRepository(AppDbContext context)
        {
            _context = context;
        }

        public OrdemServico Criar(OrdemServico ordemServico)
        {
            _context.Add(ordemServico);
            _context.SaveChanges();
            return ordemServico;
        }

        public void Atualizar(OrdemServico ordemServico)
        {
            _context.ChangeTracker.Clear();
            OrdemServico os = _context.OrdemServico.FirstOrDefault(os => os.CodOS == ordemServico.CodOS);
            if (ordemServico.Tecnico != null)
                ordemServico.Tecnico.OrdensServico = null;

            if (ordemServico.Filial != null)
                ordemServico.Filial.OrdensServico  = null;

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

        public List<ViewExportacaoChamadosUnificado> ObterViewPorOs(int[] osList)
        {
            IQueryable<ViewExportacaoChamadosUnificado> query = _context.ViewExportacaoChamadosUnificado.AsQueryable();

            query = query.Where(q => osList.Contains(q.CodOS.Value) );

            return PagedList<ViewExportacaoChamadosUnificado>.ToPagedList(query, 1 , 100000);
        }

        public OrdemServico ObterPorCodigo(int codigo) {
			return _context.OrdemServico
                .Include(os => os.Chamado!)
                    .ThenInclude(ch => ch.OperadoraTelefonia)
                    .DefaultIfEmpty()
                .Include(os => os.Chamado!)
                    .ThenInclude(ch => ch.ChamadoDadosAdicionais)
                    .DefaultIfEmpty()
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
                .Include(os => os.EquipamentoContrato)
                    .ThenInclude(ec => ec.ANS)
                .Include(os => os.RegiaoAutorizada)
                .Include(os => os.RegiaoAutorizada.Filial)
                .Include(os => os.RegiaoAutorizada.Autorizada)
                .Include(os => os.RegiaoAutorizada.Regiao)
                .Include(os => os.Cliente)
                .Include(os => os.Cliente.Cidade)
                .Include(os => os.Tecnico)
                .Include(os => os.PrazosAtendimento)
                .Include(os => os.OrdensServicoRelatorioInstalacao)
                .Include(os => os.Agendamentos)
                .AsNoTracking()
                .FirstOrDefault(os => os.CodOS == codigo);
        }
    }
}