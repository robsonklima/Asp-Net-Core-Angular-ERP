using Microsoft.EntityFrameworkCore;
using SAT.API.Context;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System;

namespace SAT.API.Repositories
{
    public class OrdemServicoRepository : IOrdemServicoRepository
    {
        private readonly AppDbContext _context;

        public OrdemServicoRepository(AppDbContext context)
        {
            _context = context;
        }
        public PagedList<OrdemServico> ObterPorParametros(OrdemServicoParameters parameters)
        {
            var ordensServico = _context.OrdemServico
                .Include(os => os.StatusSLAOSFechada)
                .Include(os => os.StatusSLAOSAberta)
                .Include(os => os.StatusServico)
                .Include(os => os.TipoIntervencao)
                .Include(os => os.LocalAtendimento)
                .Include(os => os.LocalAtendimento.Cidade)
                .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
                .Include(os => os.Equipamento)
                .Include(os => os.EquipamentoContrato)
                .Include(os => os.EquipamentoContrato.AcordoNivelServico)
                .Include(os => os.Regiao)
                .Include(os => os.Autorizada)
                .Include(os => os.Filial)
                .Include(os => os.Filial.Cidade)
                .Include(os => os.Cliente)
                .Include(os => os.Cliente.Cidade)
                .Include(os => os.Tecnico)
                .Include(os => os.RelatoriosAtendimento)
                .AsQueryable();

            if (parameters.CodOS != null)
            {
                ordensServico = ordensServico.Where(os => os.CodOS == parameters.CodOS);
            }

            if (parameters.NumOSCliente != null)
            {
                ordensServico = ordensServico.Where(os => os.NumOSCliente == parameters.NumOSCliente);
            }

            if (parameters.NumOSQuarteirizada != null)
            {
                ordensServico = ordensServico.Where(os => os.NumOSQuarteirizada == parameters.NumOSQuarteirizada);
            }

            if (parameters.CodFilial != null)
            {
                ordensServico = ordensServico.Where(os => os.CodFilial == parameters.CodFilial);
            }

            if (parameters.CodEquipContrato != null)
            {
                ordensServico = ordensServico.Where(os => os.CodEquipContrato == parameters.CodEquipContrato);
            }

            if (parameters.DataAberturaInicio != DateTime.MinValue && parameters.DataAberturaFim != DateTime.MinValue)
            {
                ordensServico = ordensServico.Where(
                    os => 
                    os.DataHoraAberturaOS >= parameters.DataAberturaInicio &&
                    os.DataHoraAberturaOS <= parameters.DataAberturaFim);
            }

            if (parameters.DataFechamentoInicio != DateTime.MinValue && parameters.DataFechamentoFim != DateTime.MinValue)
            {
                ordensServico = ordensServico.Where(
                    os =>
                    os.DataHoraFechamento >= parameters.DataFechamentoInicio &&
                    os.DataHoraFechamento <= parameters.DataFechamentoFim);
            }

            if (parameters.Filter != null)
            {
                ordensServico = ordensServico.Where(
                    t =>
                    t.CodOS.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodigosStatusServico != null)
            {
                var codigosStatusServico = parameters.CodigosStatusServico.Split(',');
                string condicoes = string.Empty;

                for (int i = 0; i < codigosStatusServico.Length; i++)
                {
                    condicoes += string.Format("CodStatusServico={0}", codigosStatusServico[i]);

                    if (i < codigosStatusServico.Length - 1)
                    {
                        condicoes += " Or ";
                    }
                }

                ordensServico = ordensServico.Where(condicoes);
            }

            if (parameters.CodigosTipoIntervencao != null)
            {
                var codigosTipoIntervencao = parameters.CodigosTipoIntervencao.Split(',');
                string condicoes = string.Empty;

                for (int i = 0; i < codigosTipoIntervencao.Length; i++)
                {
                    condicoes += string.Format("CodTipoIntervencao={0}", codigosTipoIntervencao[i]);

                    if (i < codigosTipoIntervencao.Length - 1)
                    {
                        condicoes += " Or ";
                    }
                }

                ordensServico = ordensServico.Where(condicoes);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                ordensServico = ordensServico.OrderBy(string.Format("{0} {1}", 
                    parameters.SortActive, parameters.SortDirection));
            }

            var a = ordensServico.ToQueryString();

            return PagedList<OrdemServico>.ToPagedList(ordensServico, parameters.PageNumber, parameters.PageSize);
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

        public OrdemServico ObterPorCodigo(int codigo)
        {
             var ordemServico = _context.OrdemServico
                .Include(os => os.StatusSLAOSFechada)
                .Include(os => os.StatusSLAOSAberta)
                .Include(os => os.StatusServico)
                .Include(os => os.TipoIntervencao)
                .Include(os => os.LocalAtendimento)
                .Include(os => os.LocalAtendimento.Cidade)
                .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
                .Include(os => os.Equipamento)
                .Include(os => os.EquipamentoContrato)
                .Include(os => os.EquipamentoContrato.AcordoNivelServico)
                .Include(os => os.EquipamentoContrato.Contrato)
                .Include(os => os.Regiao)
                .Include(os => os.Autorizada)
                .Include(os => os.Filial)
                .Include(os => os.Filial.Cidade)
                .Include(os => os.Cliente)
                .Include(os => os.Cliente.Cidade)
                .Include(os => os.Tecnico)
                .Include(os => os.Fotos)
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
                    .ThenInclude(a => a.Tecnico)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.StatusServico)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.TipoServico)
                .FirstOrDefault(os => os.CodOS == codigo);

            return ordemServico;
        }
    }
}
