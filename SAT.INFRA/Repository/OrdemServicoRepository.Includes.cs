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
                            NumReincidencia = i.NumReincidencia ?? null,
                            CodTipoIntervencao = i.CodTipoIntervencao,
                            CodEquip = i.CodEquip ?? null,
                            CodContrato = i.CodContrato ?? null,
                            CodEquipContrato = i.CodEquipContrato ?? null,
                            CodTecnico = i.CodTecnico ?? null,
                            CodStatusServico = i.CodStatusServico,
                            CodFilial = i.CodFilial ?? null,
                            CodPosto = i.CodPosto,
                            CodCliente = i.CodCliente,
                            CodAutorizada = i.CodAutorizada ?? null,
                            DataHoraAberturaOS = i.DataHoraAberturaOS ?? null,
                            DataHoraFechamento = i.DataHoraFechamento ?? null,
                            DefeitoRelatado = i.DefeitoRelatado,
                            NumOSQuarteirizada = i.NumOSQuarteirizada,
                            NumOSCliente = i.NumOSCliente,
                            CodRegiao = i.CodRegiao ?? null,
                            DataHoraTransf = i.DataHoraTransf,
                            DataHoraOSMobileLida = i.DataHoraOSMobileLida,
                            Tecnico = i.Tecnico != null ? new Tecnico
                            {
                                CodTecnico = i.Tecnico != null ? i.Tecnico.CodTecnico : null,
                                Nome = i.Tecnico != null ? i.Tecnico.Nome : null
                            } : null,
                            StatusServico = i.StatusServico != null ? new StatusServico
                            {
                                CodStatusServico = i.StatusServico.CodStatusServico,
                                NomeStatusServico = i.StatusServico.NomeStatusServico,
                                CorFundo = i.StatusServico.CorFundo,
                                CorFonte = i.StatusServico.CorFonte,
                                Abrev = i.StatusServico.Abrev
                            } : null,
                            TipoIntervencao = i.TipoIntervencao != null ? new TipoIntervencao
                            {
                                CodTipoIntervencao = i.TipoIntervencao != null ? i.TipoIntervencao.CodTipoIntervencao : null,
                                NomTipoIntervencao = i.TipoIntervencao.NomTipoIntervencao,
                                CodETipoIntervencao = i.TipoIntervencao.CodETipoIntervencao
                            } : null,
                            EquipamentoContrato = i.EquipamentoContrato != null ? new EquipamentoContrato
                            {
                                CodEquipContrato = i.EquipamentoContrato.CodEquipContrato,
                                NumSerie = i.EquipamentoContrato.NumSerie,
                                Autorizada = new Autorizada
                                {
                                    CodAutorizada = i.EquipamentoContrato != null ? i.EquipamentoContrato.Autorizada.CodAutorizada : null,
                                    NomeFantasia = i.EquipamentoContrato.Autorizada.NomeFantasia
                                },
                                Regiao = new Regiao
                                {
                                    CodRegiao = i.EquipamentoContrato.Regiao.CodRegiao,
                                    NomeRegiao = i.EquipamentoContrato.Regiao.NomeRegiao
                                },
                                AcordoNivelServico = new AcordoNivelServico
                                {
                                    CodSLA = i.EquipamentoContrato.AcordoNivelServico.CodSLA,
                                    NomeSLA = i.EquipamentoContrato.AcordoNivelServico.NomeSLA
                                }
                            } : null,
                            LocalAtendimento = i.LocalAtendimento != null ? new LocalAtendimento
                            {
                                CodPosto = i.LocalAtendimento != null ? i.LocalAtendimento.CodPosto : null,
                                NomeLocal = i.LocalAtendimento.NomeLocal
                            } : null,
                            Equipamento = i.Equipamento != null ? new Equipamento
                            {
                                NomeEquip = i.Equipamento.NomeEquip,
                            } : null,
                            RegiaoAutorizada = i.RegiaoAutorizada != null ? new RegiaoAutorizada
                            {
                                CodRegiao = i.RegiaoAutorizada != null ? i.RegiaoAutorizada.CodRegiao : null,
                                CodAutorizada = i.RegiaoAutorizada != null ? i.RegiaoAutorizada.CodAutorizada : null,
                                CodFilial = i.RegiaoAutorizada != null ? i.RegiaoAutorizada.CodFilial : null,
                                PA = i.RegiaoAutorizada != null ? i.RegiaoAutorizada.PA : null,
                            } : null,
                            Cliente = i.Cliente != null ? new Cliente
                            {
                                NomeFantasia = i.Cliente.NomeFantasia,
                                NumBanco = i.Cliente.NumBanco
                            } : null,
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