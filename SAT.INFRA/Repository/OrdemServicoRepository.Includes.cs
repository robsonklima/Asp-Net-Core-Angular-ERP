using Microsoft.EntityFrameworkCore;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;

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
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(os => os.Fotos);
                    break;

                case (OrdemServicoIncludeEnum.SLA):
                    query = query
                        .Include(os => os.RelatoriosAtendimento)
                        .Include(os => os.LocalAtendimento.Cidade)
                        .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
                        .Include(os => os.EquipamentoContrato.ANS);
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
                        .Include(os => os.Equipamento)
                        .Include(os => os.LocalAtendimento)
                            .ThenInclude(os => os.Cidade)
                                 .ThenInclude(os => os.UnidadeFederativa)
                                     .ThenInclude(os => os.DispBBRegiaoUF)
                                        .ThenInclude(os => os.DispBBRegiao);
                    break;
                case (OrdemServicoIncludeEnum.OS_ORCAMENTO):
                    query = query
                        .Include(os => os.Filial)
                            .ThenInclude(f => f.OrcamentoISS)
                        .Include(os => os.Equipamento)
                        .Include(os => os.EquipamentoContrato)
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(a => a.ProtocolosSTN)
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(a => a.RelatorioAtendimentoDetalhes)
                                    .ThenInclude(rat => rat.RelatorioAtendimentoDetalhePecas)
                                        .ThenInclude(rat => rat.Peca)
                                            .ThenInclude(peca => peca.ClientePeca)
                        .Include(os => os.RelatoriosAtendimento)
                            .ThenInclude(a => a.RelatorioAtendimentoDetalhes)
                                    .ThenInclude(rat => rat.RelatorioAtendimentoDetalhePecas)
                                        .ThenInclude(rat => rat.Peca)
                        .Include(os => os.Orcamentos)
                            .ThenInclude(orc => orc.OrcamentoMotivo)
                        .Include(os => os.LocalAtendimento);
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
                            DataManutencao = i.DataManutencao ?? null,
                            DefeitoRelatado = i.DefeitoRelatado,
                            NumOSQuarteirizada = i.NumOSQuarteirizada,
                            NumOSCliente = i.NumOSCliente,
                            CodRegiao = i.CodRegiao ?? null,
                            DataHoraTransf = i.DataHoraTransf ?? null,
                            DataHoraOSMobileLida = i.DataHoraOSMobileLida ?? null,
                            UsuarioManutencao = i.CodUsuarioManutencao != null ? new Usuario
                            {
                                NomeUsuario = i.UsuarioManutencao.NomeUsuario
                            } : null,
                            Tecnico = i.Tecnico != null ? new Tecnico
                            {
                                CodTecnico = i.Tecnico.CodTecnico,
                                Nome = i.Tecnico.Nome
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
                                CodTipoIntervencao = i.TipoIntervencao.CodTipoIntervencao,
                                NomTipoIntervencao = i.TipoIntervencao.NomTipoIntervencao,
                                CodETipoIntervencao = i.TipoIntervencao.CodETipoIntervencao,
                            } : null,
                            EquipamentoContrato = i.EquipamentoContrato != null ? new EquipamentoContrato
                            {
                                CodEquipContrato = i.EquipamentoContrato.CodEquipContrato,
                                NumSerie = i.EquipamentoContrato.NumSerie,
                                PontoEstrategico = i.EquipamentoContrato.PontoEstrategico,
                                Autorizada = i.EquipamentoContrato.Autorizada != null ? new Autorizada
                                {
                                    CodAutorizada = i.EquipamentoContrato.Autorizada.CodAutorizada,
                                    NomeFantasia = i.EquipamentoContrato.Autorizada.NomeFantasia
                                } : null,
                                Regiao = i.EquipamentoContrato.Regiao != null ? new Regiao
                                {
                                    CodRegiao = i.EquipamentoContrato.Regiao.CodRegiao,
                                    NomeRegiao = i.EquipamentoContrato.Regiao.NomeRegiao
                                } : null,
                                AcordoNivelServico = i.EquipamentoContrato.AcordoNivelServico != null ? new AcordoNivelServico
                                {
                                    CodSLA = i.EquipamentoContrato.AcordoNivelServico.CodSLA,
                                    NomeSLA = i.EquipamentoContrato.AcordoNivelServico.NomeSLA,
                                    DescSLA = i.EquipamentoContrato.AcordoNivelServico.DescSLA
                                } : null
                            } : null,
                            LocalAtendimento = i.LocalAtendimento != null ? new LocalAtendimento
                            {
                                CodPosto = i.LocalAtendimento.CodPosto,
                                NomeLocal = i.LocalAtendimento.NomeLocal,
                                Endereco = i.LocalAtendimento.Endereco,
                                Cep = i.LocalAtendimento.Cep,
                                Bairro = i.LocalAtendimento.Bairro,
                                NumeroEnd = i.LocalAtendimento.NumeroEnd,
                                DistanciaKmPatRes = i.LocalAtendimento.DistanciaKmPatRes,
                                Cidade = i.LocalAtendimento.Cidade != null ? new Cidade
                                {
                                    CodCidade = i.LocalAtendimento.Cidade.CodCidade,
                                    NomeCidade = i.LocalAtendimento.Cidade.NomeCidade,
                                    UnidadeFederativa = i.LocalAtendimento.Cidade.UnidadeFederativa != null ? new UnidadeFederativa
                                    {
                                        CodUF = i.LocalAtendimento.Cidade.UnidadeFederativa.CodUF,
                                        SiglaUF = i.LocalAtendimento.Cidade.UnidadeFederativa.SiglaUF
                                    } : null,
                                } : null,
                                Filial = i.LocalAtendimento.Filial != null ? new Filial
                                {
                                    CodFilial = i.LocalAtendimento.Filial.CodFilial,
                                    NomeFilial = i.LocalAtendimento.Filial.NomeFilial,
                                } : null,
                            } : null,
                            Regiao = i.Regiao != null ? new Regiao
                            {
                                CodRegiao = i.Regiao.CodRegiao,
                                NomeRegiao = i.Regiao.NomeRegiao
                            } : null,
                            Autorizada = i.Autorizada != null ? new Autorizada
                            {
                                CodAutorizada = i.Autorizada.CodAutorizada,
                                NomeFantasia = i.Autorizada.NomeFantasia
                            } : null,
                            Equipamento = i.Equipamento != null ? new Equipamento
                            {
                                NomeEquip = i.Equipamento.NomeEquip,
                            } : null,
                            RegiaoAutorizada = i.RegiaoAutorizada != null ? new RegiaoAutorizada
                            {
                                CodRegiao = i.RegiaoAutorizada.CodRegiao,
                                CodAutorizada = i.RegiaoAutorizada.CodAutorizada,
                                CodFilial = i.RegiaoAutorizada.CodFilial,
                                PA = i.RegiaoAutorizada.PA
                            } : null,
                            Cliente = i.Cliente != null ? new Cliente
                            {
                                NomeFantasia = i.Cliente.NomeFantasia,
                                NumBanco = i.Cliente.NumBanco
                            } : null,
                            Filial = i.Filial != null ? new Filial
                            {
                                NomeFilial = i.Filial.NomeFilial,
                                CodFilial = i.Filial.CodFilial
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

                case (OrdemServicoIncludeEnum.OS_EXPORTAR):
                    query = query
							.Include(os => os.PrazosAtendimento)
							.Include(os => os.RegiaoAutorizada)
							.Include(os => os.Filial)
							.Include(os => os.StatusServico)
							.Include(os => os.TipoIntervencao)
							.Include(os => os.Tecnico)
							.Include(os => os.Cliente)
							.Include(os => os.LocalAtendimento)
							.Include(os => os.Equipamento)
							.Include(os => os.LocalAtendimento.Cidade)
                        	.Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
							.Include(os => os.EquipamentoContrato)
								.ThenInclude(os => os.AcordoNivelServico)
							.Include(os => os.EquipamentoContrato)
                            	.ThenInclude(os => os.Contrato)
							.Include(os => os.EquipamentoContrato)
                            	.ThenInclude(os => os.Cliente)
							.Include(os => os.Regiao)
                            .Include(os => os.RelatoriosAtendimento)                                
                            	.ThenInclude(os => os.Tecnico)
                            .Include(os => os.RelatoriosAtendimento)
                                .ThenInclude(os => os.StatusServico)
                            .Include(os => os.RelatoriosAtendimento)
                                .ThenInclude(os => os.TipoServico)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.TipoCausa)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.GrupoCausa)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.Causa)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.Acao)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.Defeito)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.RelatorioAtendimentoDetalhePecas)
                                         .ThenInclude(os => os.Peca);
                    break;
                case (OrdemServicoIncludeEnum.OS_EXPORTAR_ZIP):
                    query = query
							.Include(os => os.PrazosAtendimento)
							.Include(os => os.RegiaoAutorizada)
							.Include(os => os.Filial)
							.Include(os => os.StatusServico)
							.Include(os => os.TipoIntervencao)
							.Include(os => os.Tecnico)
							.Include(os => os.Cliente)
							.Include(os => os.LocalAtendimento)
							.Include(os => os.Equipamento)
							.Include(os => os.LocalAtendimento.Cidade)
                        	.Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
							.Include(os => os.EquipamentoContrato)
								.ThenInclude(os => os.AcordoNivelServico)
							.Include(os => os.EquipamentoContrato)
                            	.ThenInclude(os => os.Contrato)
							.Include(os => os.EquipamentoContrato)
                            	.ThenInclude(os => os.Cliente)
							.Include(os => os.Regiao)
                            .Include(os => os.RelatoriosAtendimento)                                
                            	.ThenInclude(os => os.Tecnico)
                            .Include(os => os.RelatoriosAtendimento)
                                .ThenInclude(os => os.StatusServico)
                            .Include(os => os.RelatoriosAtendimento)
                                .ThenInclude(os => os.TipoServico)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.Laudos)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.Fotos)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)                                                                  
                                     .ThenInclude(os => os.TipoCausa)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.GrupoCausa)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.Causa)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.Acao)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.Defeito)
                            .Include(os => os.RelatoriosAtendimento)
                                 .ThenInclude(os => os.RelatorioAtendimentoDetalhes)
                                     .ThenInclude(os => os.RelatorioAtendimentoDetalhePecas)
                                         .ThenInclude(os => os.Peca);
                    break;

                case (OrdemServicoIncludeEnum.OS_INTEGRACAO):
                    query = query
                    .Include(os => os.StatusServico);
                    break;

                default:
                    query = query
                        .Include(os => os.StatusServico)
                        .Include(os => os.Filial)
                        .Include(os => os.TipoIntervencao)
                        .Include(os => os.LocalAtendimento)
                            .ThenInclude(l => l.Cidade)
                        .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa)
                        .Include(os => os.LocalAtendimento.Cidade.UnidadeFederativa.Pais)
                        .Include(os => os.Equipamento)
                        .Include(os => os.Agendamentos)
                        .Include(os => os.EquipamentoContrato)
                        .Include(os => os.Regiao)
                        .Include(os => os.Autorizada)
                        .Include(os => os.EquipamentoContrato.Regiao)
                        .Include(os => os.EquipamentoContrato.Autorizada)
                        .Include(os => os.EquipamentoContrato.AcordoNivelServico)
                        .Include(os => os.RegiaoAutorizada)
                        .Include(os => os.RegiaoAutorizada.Filial)
                        .Include(os => os.RegiaoAutorizada.Autorizada)
                        .Include(os => os.RegiaoAutorizada.Regiao)
                        .Include(os => os.Cliente)
                        .Include(os => os.Cliente.Cidade)
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
                        .Include(os => os.EquipamentoContrato.ANS)
                        .Include(os => os.PrazosAtendimento);
                    break;
            }

            return query;
        }
    }
}