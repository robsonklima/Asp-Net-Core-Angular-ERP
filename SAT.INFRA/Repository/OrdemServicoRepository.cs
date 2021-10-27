using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Context;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Helpers;
using System.Linq.Dynamic.Core;
using System.Linq;
using System;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
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
            var query = _context.OrdemServico.AsQueryable();

            switch (parameters.Include)
            {
                case (OrdemServicoIncludeEnum.OS_RAT):
                    query = query
                        .Include(os => os.StatusServico)
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.Tecnico)
                        .Include(os => os.AgendaTecnico)
                        .Include(os => os.RelatoriosAtendimento);
                    break;

                case (OrdemServicoIncludeEnum.OS_AGENDA):
                    query = query
                        .Include(os => os.StatusServico)
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.Tecnico)
                        .Include(os => os.LocalAtendimento)
                        .Include(os => os.AgendaTecnico)
                        .Include(os => os.RegiaoAutorizada.Autorizada)
                        .Include(os => os.RegiaoAutorizada.Regiao);
                    break;

                default:
                    query = query
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
                        .Include(os => os.AgendaTecnico)
                        .Include(os => os.Agendamentos)
                        .Include(os => os.Tecnico)
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(rat => rat.RelatorioAtendimentoDetalhes)
                                .ThenInclude(ratd => ratd.RelatorioAtendimentoDetalhePecas)
                                    .ThenInclude(ratdp => ratdp.Peca)
                        .Include(os => os.EquipamentoContrato.Contrato)
                        .Include(os => os.PrazosAtendimento);
                    break;
            }

            if (parameters.CodOS != null)
            {
                query = query.Where(os => os.CodOS == parameters.CodOS);
            }

            if (parameters.NumOSCliente != null)
            {
                query = query.Where(os => os.NumOSCliente == parameters.NumOSCliente);
            }

            if (!string.IsNullOrEmpty(parameters.PAS))
            {
                var pas = parameters.PAS.Split(",");
                query = query.Where(os => pas.Any(p => p == os.RegiaoAutorizada.PA.ToString()));
            }

            if (parameters.NumOSQuarteirizada != null)
            {
                query = query.Where(os => os.NumOSQuarteirizada == parameters.NumOSQuarteirizada);
            }

            if (parameters.CodTecnico != null)
            {
                query = query.Where(os => os.CodTecnico == parameters.CodTecnico);
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

            if (parameters.DataTransfInicio != DateTime.MinValue && parameters.DataTransfFim != DateTime.MinValue)
            {
                query = query.Where(os => os.DataHoraTransf >= parameters.DataTransfInicio && os.DataHoraTransf <= parameters.DataTransfFim);
            }

            if (parameters.Filter != null)
            {
                query = query.Where(
                    t =>
                    t.CodOS.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Cliente.NumBanco.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Cliente.NomeFantasia.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (parameters.CodTiposGrupo != null)
            {
                query = query.Where(
                    os =>
                    os.EquipamentoContrato != null &&
                    parameters.CodTiposGrupo.Contains(os.EquipamentoContrato.CodTipoEquip.ToString())
                );
            }

            if (parameters.CodStatusServicos != null)
            {
                var statusServicos = parameters.CodStatusServicos.Split(",").Select(p => p.Trim());
                query = query.Where(os => statusServicos.Any(r => r == os.CodStatusServico.ToString()));
            }

            if (parameters.CodRegioes != null)
            {
                var regioes = parameters.CodRegioes.Split(",");
                query = query.Where(os => regioes.Any(r => r == os.RegiaoAutorizada.CodRegiao.ToString()));
            }

            if (parameters.CodTecnicos != null)
            {
                var tecnicos = parameters.CodTecnicos.Split(",");
                query = query.Where(os => tecnicos.Any(r => r == os.CodTecnico.ToString()));
            }

            if (parameters.CodTiposIntervencao != null)
            {
                var tiposIntervencao = parameters.CodTiposIntervencao.Split(",");
                query = query.Where(os => tiposIntervencao.Any(r => r == os.TipoIntervencao.CodTipoIntervencao.ToString()));
            }

            if (parameters.CodClientes != null)
            {
                var clientes = parameters.CodClientes.Split(",");
                query = query.Where(os => clientes.Any(r => r == os.CodCliente.ToString()));
            }

            if (parameters.CodEquipamentos != null)
            {
                var equipamentos = parameters.CodEquipamentos.Split(",");
                query = query.Where(os => equipamentos.Any(r => r == os.CodEquip.ToString()));
            }

            if (parameters.Equipamento != null)
            {
                query = query.Where(
                    os => !string.IsNullOrEmpty(os.Equipamento.NomeEquip) &&
                    (os.Equipamento.CodEquip.ToString().Contains(parameters.Equipamento) ||
                    os.Equipamento.NomeEquip.Contains(parameters.Equipamento)));
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(',').Select(f => f.Trim());
                query = query.Where(os => filiais.Any(p => p == os.CodFilial.ToString()));
            }

            if (parameters.CodAutorizadas != null)
            {
                var autorizadas = parameters.CodAutorizadas.Split(",");
                query = query.Where(os => autorizadas.Any(r => r == os.CodAutorizada.ToString()));
            }

            if (parameters.PontosEstrategicos != null)
            {
                var paramsSplit = parameters.PontosEstrategicos.Split(',');
                query = query.Where(os => paramsSplit.Any(p => p == os.EquipamentoContrato.PontoEstrategico));
            }

            if (parameters.IsAgendaTecnico)
            {
                query = query.Where(os => os.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO ||
                (os.CodStatusServico == (int)StatusServicoEnum.ABERTO || os.CodStatusServico == (int)StatusServicoEnum.FECHADO && (os.DataHoraTransf.Value.Date == DateTime.Now.Date || os.DataHoraFechamento.Value.Date == DateTime.Now.Date)));
            }

            if (parameters.SortActive != null && parameters.SortDirection != null)
            {
                var property = parameters.SortActive;

                switch (property)
                {
                    case "fimSLA":
                        query = parameters.SortDirection == "asc" ?
                        query.Where(q => q.PrazosAtendimento.Any())
                             .OrderBy(q => q.PrazosAtendimento
                             .OrderBy(pa => pa.DataHoraLimiteAtendimento)
                             .Select(pa => pa.DataHoraLimiteAtendimento)
                             .FirstOrDefault()) :
                        query.Where(q => q.PrazosAtendimento.Any())
                             .OrderByDescending(q => q.PrazosAtendimento
                             .OrderByDescending(pa => pa.DataHoraLimiteAtendimento)
                             .Select(pa => pa.DataHoraLimiteAtendimento)
                             .FirstOrDefault());
                        break;

                    case "statusOS":
                        query = parameters.SortDirection == "asc" ?
                            query.OrderBy(q => q.StatusServico.Abrev) :
                            query.OrderByDescending(q => q.StatusServico.Abrev);
                        break;

                    case "nomeRegiao":
                        query = parameters.SortDirection == "asc" ?
                            query.OrderBy(q => q.EquipamentoContrato.Regiao.NomeRegiao) :
                            query.OrderByDescending(q => q.EquipamentoContrato.Regiao.NomeRegiao);
                        break;

                    case "pa":
                        query = parameters.SortDirection == "asc" ?
                            query.OrderBy(q => q.RegiaoAutorizada.PA.ToString()) :
                            query.OrderByDescending(q => q.RegiaoAutorizada.PA.ToString());
                        break;
                    case "nomeLocal":
                        query = parameters.SortDirection == "asc" ?
                            query.OrderBy(q => q.LocalAtendimento.NomeLocal) :
                            query.OrderByDescending(q => q.LocalAtendimento.NomeLocal);
                        break;

                    case "numBanco":
                        query = parameters.SortDirection == "asc" ?
                            query.Where(q => !string.IsNullOrEmpty(q.Cliente.NumBanco))
                                 .OrderBy(q => q.Cliente.NumBanco) :
                            query.Where(q => !string.IsNullOrEmpty(q.Cliente.NumBanco))
                                 .OrderByDescending(q => q.Cliente.NumBanco);
                        break;

                    case "nomeEquip":
                        query = parameters.SortDirection == "asc" ?
                            query.Where(q => !string.IsNullOrEmpty(q.Equipamento.NomeEquip))
                                 .OrderBy(q => q.Equipamento.NomeEquip) :
                        query.Where(q => !string.IsNullOrEmpty(q.Equipamento.NomeEquip))
                             .OrderByDescending(q => q.Equipamento.NomeEquip);
                        break;

                    case "nomeSLA":
                        query = parameters.SortDirection == "asc" ?
                            query.Where(q => !string.IsNullOrEmpty(q.EquipamentoContrato.AcordoNivelServico.NomeSLA))
                                 .OrderBy(q => q.EquipamentoContrato.AcordoNivelServico.NomeSLA) :
                            query.Where(q => !string.IsNullOrEmpty(q.EquipamentoContrato.AcordoNivelServico.NomeSLA))
                                .OrderByDescending(q => q.EquipamentoContrato.AcordoNivelServico.NomeSLA);
                        break;

                    case "numSerie":
                        query = parameters.SortDirection == "asc" ?
                            query.Where(q => !string.IsNullOrEmpty(q.EquipamentoContrato.NumSerie))
                                 .OrderBy(q => q.EquipamentoContrato.NumSerie) :
                            query.Where(q => !string.IsNullOrEmpty(q.EquipamentoContrato.NumSerie))
                                 .OrderByDescending(q => q.EquipamentoContrato.NumSerie);
                        break;

                    case "nomeTecnico":
                        query = parameters.SortDirection == "asc" ?
                            query.Where(q => !string.IsNullOrEmpty(q.Tecnico.Nome) && q.CodStatusServico == 8)
                                 .OrderBy(q => q.Tecnico.Nome) :
                            query.Where(q => !string.IsNullOrEmpty(q.Tecnico.Nome) && q.CodStatusServico == 8)
                                 .OrderByDescending(q => q.Tecnico.Nome);
                        break;

                    default:
                        query = query.OrderBy(string.Format("{0} {1}", parameters.SortActive, parameters.SortDirection));
                        break;
                }
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
                    .ThenInclude(a => a.Tecnico)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.StatusServico)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.TipoServico)
                .Include(os => os.RelatoriosAtendimento)
                    .ThenInclude(a => a.CheckinsCheckouts)
                .Include(os => os.OrdensServicoRelatorioInstalacao)
                .FirstOrDefault(os => os.CodOS == codigo);

            return ordemServico;
        }
    }
}
