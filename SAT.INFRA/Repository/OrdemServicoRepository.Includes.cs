using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;
using System.Collections.Generic;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarIncludes(IQueryable<OrdemServico> query, OrdemServicoIncludeEnum include)
        {
            switch (include)
            {
                case (OrdemServicoIncludeEnum.OS_RAT):
                    query = query
                        .Include(os => os.StatusServico)
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.Tecnico)
                        .Include(os => os.AgendaTecnico)
                        .Include(os => os.RelatoriosAtendimento);
                    break;

                case (OrdemServicoIncludeEnum.OS_PECAS):
                    query = query
                        .Include(os => os.Filial)
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                .ThenInclude(os => os.RelatorioAtendimentoDetalhePecas)
                                    .ThenInclude(os => os.RelatorioAtendimentoDetalhePecaStatus)
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                .ThenInclude(os => os.RelatorioAtendimentoDetalhePecas)
                                    .ThenInclude(os => os.Peca)
                        .Include(os => os.PrazosAtendimento);
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

                case (OrdemServicoIncludeEnum.OS_EQUIPAMENTOS):
                    query = query
                        .Include(os => os.Filial)
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.Cliente)
                        .Include(os => os.Equipamento)
                        .Include(os => os.EquipamentoContrato);
                    break;

                case (OrdemServicoIncludeEnum.OS_EQUIPAMENTOS_ATENDIMENTOS):
                    query = query
                        .Include(os => os.Filial)
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.Cliente)
                        .Include(os => os.Equipamento)
                        .Include(os => os.EquipamentoContrato)
                        .Include(os => os.PrazosAtendimento);
                    break;

                case (OrdemServicoIncludeEnum.OS_TECNICOS):
                    query = query
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(r => r.StatusServico);
                    break;

                case (OrdemServicoIncludeEnum.OS_RAT_FILIAL_PRAZOS_ATENDIMENTO):
                    query = query
                        .Include(os => os.RelatoriosAtendimento)
                        .Include(os => os.Filial)
                        .Include(os => os.PrazosAtendimento);
                    break;

                case (OrdemServicoIncludeEnum.OS_RAT_CLIENTE_PRAZOS_ATENDIMENTO):
                    query = query
                        .Include(os => os.RelatoriosAtendimento)
                        .Include(os => os.Cliente)
                        .Include(os => os.PrazosAtendimento);
                    break;

                case (OrdemServicoIncludeEnum.OS_TECNICO_ATENDIMENTO):
                    query = query
                        .Include(os => os.Tecnico)
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(os => os.Tecnico)
                        .Include(os => os.PrazosAtendimento);
                    break;
                case (OrdemServicoIncludeEnum.OS_DISPONIBILIDADE_BB):
                    query = query
                        .Include(os => os.RelatoriosAtendimento)
                        .Include(os => os.Filial)
                        .Include(os => os.DispBBEquipamentoContrato)
                            .ThenInclude(os => os.Equipamento)
                        .Include(os => os.DispBBEquipamentoContrato)
                            .ThenInclude(os => os.DispBBCriticidade)
                              .Include(os => os.DispBBEquipamentoContrato)
                            .ThenInclude(os => os.AcordoNivelServico)
                        .Include(os => os.Contrato)
                        .Include(os => os.EquipamentoContrato)
                        // .ThenInclude(os => os.AcordoNivelServico)
                        .Include(os => os.Equipamento)
                        .Include(os => os.LocalAtendimento)
                            .ThenInclude(os => os.Cidade)
                                 .ThenInclude(os => os.UnidadeFederativa)
                                     .ThenInclude(os => os.DispBBRegiaoUF)
                                        .ThenInclude(os => os.DispBBRegiao);
                    break;
                case (OrdemServicoIncludeEnum.OS_LISTA):
                    query = query
                        .Select(i => new OrdemServico
                        {
                            CodOS = i.CodOS,
                            NumReincidencia = i.NumReincidencia,
                            CodTipoIntervencao = i.CodTipoIntervencao,
                            CodEquip = i.CodEquip,
                            CodContrato = i.CodContrato,
                            CodEquipContrato = i.CodEquipContrato,
                            CodTecnico = i.CodTecnico,
                            CodStatusServico = i.CodStatusServico,
                            CodFilial = i.CodFilial,
                            CodPosto = i.CodPosto,
                            CodCliente = i.CodCliente,
                            CodAutorizada = i.CodAutorizada,
                            DataHoraAberturaOS = i.DataHoraAberturaOS,
                            DataHoraFechamento = i.DataHoraFechamento,
                            DefeitoRelatado = i.DefeitoRelatado,
                            NumOSQuarteirizada = i.NumOSQuarteirizada,
                            NumOSCliente = i.NumOSCliente,
                            Tecnico = new Tecnico
                            {
                                CodTecnico = i.Tecnico.CodTecnico,
                                Nome = i.Tecnico.Nome
                            },
                            StatusServico = new StatusServico
                            {
                                CodStatusServico = i.StatusServico.CodStatusServico,
                                NomeStatusServico = i.StatusServico.NomeStatusServico,
                                CorFundo = i.StatusServico.CorFundo,
                                CorFonte = i.StatusServico.CorFonte,
                                Abrev = i.StatusServico.Abrev
                            },
                            TipoIntervencao = new TipoIntervencao
                            {
                                NomTipoIntervencao = i.TipoIntervencao.NomTipoIntervencao,
                                CodETipoIntervencao = i.TipoIntervencao.CodETipoIntervencao
                            },
                            EquipamentoContrato = new EquipamentoContrato
                            {
                                NumSerie = i.EquipamentoContrato.NumSerie,
                                Autorizada = new Autorizada
                                {
                                    NomeFantasia = i.EquipamentoContrato.Autorizada.NomeFantasia
                                },
                                Regiao = new Regiao
                                {
                                    NomeRegiao = i.EquipamentoContrato.Regiao.NomeRegiao
                                },
                                AcordoNivelServico = new AcordoNivelServico
                                {
                                    NomeSLA = i.EquipamentoContrato.AcordoNivelServico.NomeSLA
                                }
                            },
                            LocalAtendimento = new LocalAtendimento
                            {
                                NomeLocal = i.LocalAtendimento.NomeLocal
                            },
                            Equipamento = new Equipamento
                            {
                                NomeEquip = i.Equipamento.NomeEquip,
                            },
                            RegiaoAutorizada = new RegiaoAutorizada
                            {
                                PA = i.RegiaoAutorizada.PA,
                            },
                            Cliente = new Cliente
                            {
                                NomeFantasia = i.Cliente.NomeFantasia,
                                NumBanco = i.Cliente.NumBanco
                            },
                            Agendamentos = i.Agendamentos
                            .OrderByDescending(i => i.CodAgendamento)
                            .Select(i => new Agendamento
                            {
                                CodAgendamento = i.CodAgendamento,
                                CodMotivo = i.CodMotivo,
                                MotivoAgendamento = i.MotivoAgendamento,
                                DataAgendamento = i.DataAgendamento
                            }).ToList(),
                            RelatoriosAtendimento = i.RelatoriosAtendimento
                            .OrderByDescending(i => i.CodRAT)
                            .Select(i => new RelatorioAtendimento
                            {
                                CodRAT = i.CodRAT,
                                CodTecnico = i.CodTecnico,
                                DataHoraSolucao = i.DataHoraSolucao
                            }).ToList(),
                            PrazosAtendimento = i.PrazosAtendimento
                            .OrderByDescending(i => i.CodOSPrazoAtendimento)
                            .Select(i => new OSPrazoAtendimento
                            {
                                CodOSPrazoAtendimento = i.CodOSPrazoAtendimento,
                                DataHoraLimiteAtendimento = i.DataHoraLimiteAtendimento
                            }).ToList(),
                        });
                    break;
                default:
                    query = query
                        .Include(os => os.StatusServico)
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.LocalAtendimento)
                        .Include(os => os.LocalAtendimento.Cidade)
                        .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
                        .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa.Pais)
                        .Include(os => os.Equipamento)
                        .Include(os => os.EquipamentoContrato)
                        .Include(os => os.EquipamentoContrato.Regiao)
                        .Include(os => os.EquipamentoContrato.Autorizada)
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
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(a => a.StatusServico)
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(a => a.Tecnico)
                        .Include(os => os.EquipamentoContrato.Contrato)
                        .Include(os => os.PrazosAtendimento);
                    break;
            }

            return query;
        }
    }
}