using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System;
using System.Collections.Generic;

namespace SAT.INFRA.Repositories
{
    public class OrdemServicoRepository : IOrdemServicoRepository
    {
        private readonly AppDbContext _context;

        public OrdemServicoRepository(AppDbContext context)
        {
            _context = context;
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
            var query = _context.OrdemServico
                .Include(os => os.StatusServico)
                .Include(os => os.TipoIntervencao)
                .Include(os => os.LocalAtendimento)
                .Include(os => os.LocalAtendimento.Cidade)
                .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
                .Include(os => os.Equipamento)
                .Include(os => os.EquipamentoContrato)
                .Include(os => os.EquipamentoContrato.AcordoNivelServico)
                .Include(os => os.RegiaoAutorizada)
                .Include(os => os.RegiaoAutorizada.Filial)
                .Include(os => os.RegiaoAutorizada.Autorizada)
                .Include(os => os.RegiaoAutorizada.Regiao)
                .Include(os => os.Cliente)
                .Include(os => os.Cliente.Cidade)
                .Include(os => os.Tecnico)
                .Include(os => os.RelatoriosAtendimento)
                .Include(os => os.EquipamentoContrato.Contrato)
                .Include(os => os.Fotos)
                .Include(os => os.Agendamentos)
                .Include(os => os.PrazosAtendimento)
                .AsQueryable();

            if (parameters.CodOS != null)
            {
                query = query.Where(os => os.CodOS == parameters.CodOS);
            }

            if (parameters.NumOSCliente != null)
            {
                query = query.Where(os => os.NumOSCliente == parameters.NumOSCliente);
            }

            if (parameters.PA != null)
            {
                query = query.Where(os => os.RegiaoAutorizada.PA == parameters.PA);
            }

            if (parameters.NumOSQuarteirizada != null)
            {
                query = query.Where(os => os.NumOSQuarteirizada == parameters.NumOSQuarteirizada);
            }

            if (parameters.CodEquipContrato != null)
            {
                query = query.Where(os => os.CodEquipContrato == parameters.CodEquipContrato);
            }

            if (parameters.DataAberturaInicio != DateTime.MinValue && parameters.DataAberturaFim != DateTime.MinValue)
            {
                query = query.Where(
                    os =>
                    os.DataHoraAberturaOS >= parameters.DataAberturaInicio &&
                    os.DataHoraAberturaOS <= parameters.DataAberturaFim);
            }

            if (parameters.DataFechamentoInicio != DateTime.MinValue && parameters.DataFechamentoFim != DateTime.MinValue)
            {
                query = query.Where(
                    os =>
                    os.DataHoraFechamento >= parameters.DataFechamentoInicio &&
                    os.DataHoraFechamento <= parameters.DataFechamentoFim);
            }

            if (parameters.Filter != null)
            {
                query = query.Where(
                    t =>
                    t.CodOS.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodStatusServicos != null)
            {
                var paramsSplit = parameters.CodStatusServicos.Split(',');
                paramsSplit = paramsSplit.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                string condicoes = string.Empty;

                for (int i = 0; i < paramsSplit.Length; i++)
                {
                    condicoes += string.Format("CodStatusServico={0}", paramsSplit[i]);

                    if (i < paramsSplit.Length - 1)
                    {
                        condicoes += " Or ";
                    }
                }

                query = query.Where(condicoes);
            }

            if (parameters.CodTiposIntervencao != null)
            {
                var paramsSplit = parameters.CodTiposIntervencao.Split(',');
                paramsSplit = paramsSplit.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                string condicoes = string.Empty;

                for (int i = 0; i < paramsSplit.Length; i++)
                {
                    condicoes += string.Format("CodTipoIntervencao={0}", paramsSplit[i]);

                    if (i < paramsSplit.Length - 1)
                    {
                        condicoes += " Or ";
                    }
                }

                query = query.Where(condicoes);
            }

            if (parameters.CodClientes != null)
            {
                var paramsSplit = parameters.CodClientes.Split(',');
                paramsSplit = paramsSplit.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                string condicoes = string.Empty;

                for (int i = 0; i < paramsSplit.Length; i++)
                {
                    condicoes += string.Format("CodCliente={0}", paramsSplit[i]);

                    if (i < paramsSplit.Length - 1)
                    {
                        condicoes += " Or ";
                    }
                }

                query = query.Where(condicoes);
            }

            if (parameters.CodFiliais != null)
            {
                var paramsSplit = parameters.CodFiliais.Split(',');
                paramsSplit = paramsSplit.Where(x => !string.IsNullOrEmpty(x)).ToArray();
                string condicoes = string.Empty;

                for (int i = 0; i < paramsSplit.Length; i++)
                {
                    condicoes += string.Format("CodFilial={0}", paramsSplit[i]);

                    if (i < paramsSplit.Length - 1)
                    {
                        condicoes += " Or ";
                    }
                }

                query = query.Where(condicoes);
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
            }

            return PagedList<OrdemServico>.ToPagedList(query, parameters.PageNumber, parameters.PageSize);
        }

        public OrdemServico ObterPorCodigo(int codigo)
        {
            var ordemServico = _context.OrdemServico
                .Include(os => os.StatusServico)
                .Include(os => os.TipoIntervencao)
                .Include(os => os.LocalAtendimento)
                .Include(os => os.LocalAtendimento.Cidade)
                .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
                .Include(os => os.Equipamento)
                .Include(os => os.EquipamentoContrato)
                .Include(os => os.EquipamentoContrato.AcordoNivelServico)
                .Include(os => os.RegiaoAutorizada)
                .Include(os => os.RegiaoAutorizada.Filial)
                .Include(os => os.RegiaoAutorizada.Autorizada)
                .Include(os => os.RegiaoAutorizada.Regiao)
                .Include(os => os.Cliente)
                .Include(os => os.Cliente.Cidade)
                .Include(os => os.Tecnico)
                .Include(os => os.RelatoriosAtendimento)
                .Include(os => os.EquipamentoContrato.Contrato)
                .Include(os => os.Fotos)
                .Include(os => os.Agendamentos)
                .Include(os => os.PrazosAtendimento)
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

        public IEnumerable<OrdemServico> ObterTodos()
        {
            return _context.OrdemServico
                .Include(os => os.TipoIntervencao)
                .Include(os => os.StatusServico)
                .Include(os => os.Filial)
                .Include(os => os.Cliente)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.Tecnico);
        }
    }
}
