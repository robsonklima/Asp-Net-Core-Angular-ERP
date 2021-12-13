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