using SAT.MODELS.Entities;
using System.Linq;
using System;
using SAT.MODELS.Entities.Params;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Enums;
using SAT.MODELS.Views;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarFiltroPadrao(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Filter))
                query = query.Where(t =>
                    t.CodOS.ToString().Contains(parameters.Filter) ||
                    t.LocalAtendimento.NomeLocal.Contains(parameters.Filter) ||
                    t.Cliente.NumBanco.Contains(parameters.Filter) ||
                    t.Cliente.NomeFantasia.Contains(parameters.Filter) ||
                    t.DefeitoRelatado.Contains(parameters.Filter) ||
                    t.NumOSCliente.Contains(parameters.Filter) ||
                    t.CodContrato.ToString().Contains(parameters.Filter));

            if (parameters.CodContrato.HasValue)
                query = query.Where(os => os.CodContrato == parameters.CodContrato.Value);

            if (parameters.CodCliente.HasValue)
                query = query.Where(os => os.CodCliente == parameters.CodCliente.Value);

            if (parameters.IndServico.HasValue)
                query = query.Where(os => os.IndServico == parameters.IndServico.Value);

            if (parameters.CodTecnico.HasValue)
                query = query.Where(os => os.CodTecnico == parameters.CodTecnico || os.RelatoriosAtendimento.Any(r => r.CodTecnico == parameters.CodTecnico));

            if (parameters.CodEquipContrato.HasValue)
                query = query.Where(os => os.CodEquipContrato == parameters.CodEquipContrato);

            if (parameters.IndIntegracao.HasValue)
                query = query.Where(os => os.IndIntegracao == parameters.IndIntegracao);

            if (parameters.DataHoraInicioInicio.HasValue && parameters.DataHoraInicioFim.HasValue)
                query = query.Where(os => os.RelatoriosAtendimento.Any(r => r.DataHoraInicio.Date >= parameters.DataHoraInicioInicio.Value.Date &&
                                    r.DataHoraInicio.Date <= parameters.DataHoraInicioFim.Value.Date));

            if (!string.IsNullOrWhiteSpace(parameters.NumOSCliente))
                query = query.Where(os => os.NumOSCliente == parameters.NumOSCliente);

            if (!string.IsNullOrWhiteSpace(parameters.NumRAT))
                query = query.Where(os => os.RelatoriosAtendimento.Any(r => r.NumRAT == parameters.NumRAT));

            if (!string.IsNullOrWhiteSpace(parameters.CodOSs))
            {
                int[] cods = parameters.CodOSs.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(q => cods.Contains(q.CodOS));
            }

            if (!string.IsNullOrWhiteSpace(parameters.NumOSQuarteirizada))
                query = query.Where(os => os.NumOSQuarteirizada == parameters.NumOSQuarteirizada);

            if (parameters.DataHoraManutInicio.HasValue && parameters.DataHoraManutFim.HasValue)
                query = query.Where(os => os.DataHoraManut.HasValue && os.DataHoraManut.Value.Date >= parameters.DataHoraManutInicio.Value.Date
                    && os.DataHoraManut.Value.Date <= parameters.DataHoraManutFim.Value.Date);

            if (parameters.DataAberturaInicio.HasValue && parameters.DataAberturaFim.HasValue)
                query = query.Where(os => os.DataHoraAberturaOS.HasValue && os.DataHoraAberturaOS.Value.Date >= parameters.DataAberturaInicio.Value.Date
                    && os.DataHoraAberturaOS.Value.Date <= parameters.DataAberturaFim.Value.Date);

            if (parameters.DataFechamentoInicio.HasValue && parameters.DataFechamentoFim.HasValue)
                query = query.Where(os => os.DataHoraFechamento.HasValue && os.DataHoraFechamento.Value.Date >= parameters.DataFechamentoInicio.Value.Date
                    && os.DataHoraFechamento.Value.Date <= parameters.DataFechamentoFim.Value.Date);

            if (parameters.DataTransfInicio.HasValue && parameters.DataTransfFim.HasValue)
                query = query.Where(os => os.DataHoraTransf.HasValue && os.DataHoraTransf.Value.Date >= parameters.DataTransfInicio.Value.Date
                    && os.DataHoraTransf.Value.Date <= parameters.DataTransfFim.Value.Date);

            if (parameters.DataHoraSolucaoInicio.HasValue && parameters.DataHoraSolucaoFim.HasValue)
                query = query.Where(os => os.RelatoriosAtendimento.Any(r => r.DataHoraSolucao.Date >= parameters.DataHoraSolucaoInicio.Value.Date &&
                   r.DataHoraSolucao.Date <= parameters.DataHoraSolucaoFim.Value.Date));

            if (parameters.DataInicioDispBB.HasValue && parameters.DataFimDispBB.HasValue)
                query = query.Where(os => os.DataHoraAberturaOS >= parameters.DataInicioDispBB
                    && os.DataHoraAberturaOS <= parameters.DataFimDispBB);

            if (!string.IsNullOrWhiteSpace(parameters.PAS))
            {
                int[] pas = parameters.PAS.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => pas.Contains(os.RegiaoAutorizada.PA.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodTiposGrupo))
            {
                query = query.Where(os => os.EquipamentoContrato != null
                    && parameters.CodTiposGrupo.Contains(os.EquipamentoContrato.CodTipoEquip.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodPostos))
            {
                int[] postos = parameters.CodPostos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => postos.Contains(os.CodPosto));
            }

            if (!string.IsNullOrWhiteSpace(parameters.NotIn_CodStatusServicos))
            {
                int[] cods = parameters.NotIn_CodStatusServicos.Split(",").Select(a => int.Parse(a.Trim())).ToArray();
                query = query.Where(os => !cods.Contains(os.CodStatusServico));
            }

            if (!string.IsNullOrEmpty(parameters.CodTipoIntervencaoNotIn))
            {
                int[] codigos = parameters.CodTipoIntervencaoNotIn.Split(',').Select(f => int.Parse(f.Trim())).Distinct().ToArray();
                query = query.Where(e => !codigos.Contains(e.CodTipoIntervencao));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodOS))
            {
                int[] cods = parameters.CodOS.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodOS));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodStatusServicos))
            {
                int[] cods = parameters.CodStatusServicos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodStatusServico));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodRegioes))
            {
                int[] cods = parameters.CodRegioes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.RegiaoAutorizada.CodRegiao.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.NumSerie))
                query = query.Where(os => os.EquipamentoContrato.NumSerie.Trim().ToLower().Contains(parameters.NumSerie.Trim().ToLower()));

            if (!string.IsNullOrWhiteSpace(parameters.Defeito))
                query = query.Where(os => os.DefeitoRelatado.Contains(parameters.Defeito));

            if (!string.IsNullOrWhiteSpace(parameters.Solucao))
                query = query.Where(os => os.RelatoriosAtendimento.Any(r => r.RelatoSolucao.Contains(parameters.Solucao)));

            if (!string.IsNullOrWhiteSpace(parameters.CodTecnicos))
            {
                int[] cods = parameters.CodTecnicos.Split(",").Select(a => int.Parse(a.Trim())).Where(s => s > 0).Distinct().ToArray();

                if (parameters.Include == OrdemServicoIncludeEnum.OS_LISTA)
                    query = query.Where(os => (os.CodTecnico.HasValue && cods.Contains(os.CodTecnico.Value))
                        || cods.Contains(os.RelatoriosAtendimento.FirstOrDefault().CodTecnico.Value));
                else
                    query = query.Where(os => os.CodTecnico.HasValue && cods.Contains(os.CodTecnico.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodUsuariosSTN))
            {
                int[] cods = parameters.CodUsuariosSTN.Split(",").Select(a => int.Parse(a.Trim())).Where(s => s > 0).Distinct().ToArray();

                if (parameters.Include == OrdemServicoIncludeEnum.OS_LISTA)
                    query = query.Where(os => (os.CodTecnico.HasValue && cods.Contains(os.CodTecnico.Value))
                        || cods.Contains(os.RelatoriosAtendimento.FirstOrDefault().CodTecnico.Value));
                else
                    query = query.Where(os => os.CodTecnico.HasValue && cods.Contains(os.CodTecnico.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodTiposIntervencao))
            {
                int[] cods = parameters.CodTiposIntervencao.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.TipoIntervencao.CodTipoIntervencao.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodCliente));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodEquipamentos))
            {
                int[] cods = parameters.CodEquipamentos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodEquip.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodFilial.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodAutorizadas))
            {
                int[] cods = parameters.CodAutorizadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodAutorizada.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.PontosEstrategicos))
            {
                string[] cods = parameters.PontosEstrategicos.Split(",").Select(a => a.Trim()).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.EquipamentoContrato.PontoEstrategico));
            }

            if (parameters.DataHoraManutNull)
                query = query.Where(os => os.DataHoraManut == null);

            
            if (parameters.CodEquipIsNull)
                query = query.Where(os => os.CodEquip == null);
            
            if (parameters.CodEquipContratoIsNotNull)
                query = query.Where(os => os.CodEquipContrato != null);

            return query;
        }

        public IQueryable<ViewExportacaoChamadosUnificado> AplicarFiltroPadraoView(IQueryable<ViewExportacaoChamadosUnificado> query, OrdemServicoParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Filter))
                query = query.Where(t =>
                    t.CodOS.ToString().Contains(parameters.Filter) ||
                    t.LocalAtendimento.Contains(parameters.Filter) ||
                    t.NumBanco.Contains(parameters.Filter) ||
                    t.Cliente.Contains(parameters.Filter) ||
                    t.NumOSCliente.Contains(parameters.Filter));

            if (parameters.CodContrato.HasValue)
                query = query.Where(os => os.CodContrato == parameters.CodContrato.Value);

            if (parameters.IndServico.HasValue)
                query = query.Where(os => os.IndServico == parameters.IndServico.Value);

            if (parameters.CodTecnico.HasValue)
                query = query.Where(os => os.CodTecnico == parameters.CodTecnico);

            if (parameters.CodEquipContrato.HasValue)
                query = query.Where(os => os.CodEquipContrato == parameters.CodEquipContrato);

            if (parameters.DataHoraInicioInicio.HasValue && parameters.DataHoraInicioFim.HasValue)
                query = query.Where(os => os.DataHoraInicio.Value.Date >= parameters.DataHoraInicioInicio.Value.Date &&
                                    os.DataHoraInicio.Value.Date <= parameters.DataHoraInicioFim.Value.Date);

            if (!string.IsNullOrWhiteSpace(parameters.NumOSCliente))
                query = query.Where(os => os.NumOSCliente == parameters.NumOSCliente);

            if (!string.IsNullOrWhiteSpace(parameters.NumOSQuarteirizada))
                query = query.Where(os => os.NumOSQuarteirizada == parameters.NumOSQuarteirizada);

            if (parameters.DataAberturaInicio.HasValue && parameters.DataAberturaFim.HasValue)
                query = query.Where(os => os.DataHoraAberturaOS.HasValue && os.DataHoraAberturaOS.Value.Date >= parameters.DataAberturaInicio.Value.Date
                    && os.DataHoraAberturaOS.Value.Date <= parameters.DataAberturaFim.Value.Date);

            if (parameters.DataFechamentoInicio.HasValue && parameters.DataFechamentoFim.HasValue)
                query = query.Where(os => os.DataHoraFechamento.HasValue && os.DataHoraFechamento.Value.Date >= parameters.DataFechamentoInicio.Value.Date
                    && os.DataHoraFechamento.Value.Date <= parameters.DataFechamentoFim.Value.Date);

            if (parameters.DataTransfInicio.HasValue && parameters.DataTransfFim.HasValue)
                query = query.Where(os => os.DataHoraTransf.HasValue && os.DataHoraTransf.Value.Date >= parameters.DataTransfInicio.Value.Date
                    && os.DataHoraTransf.Value.Date <= parameters.DataTransfFim.Value.Date);

            if (parameters.DataHoraSolucaoInicio.HasValue && parameters.DataHoraSolucaoFim.HasValue)
                query = query.Where(os => os.DataHoraSolucao.Value.Date >= parameters.DataHoraSolucaoInicio.Value.Date &&
                   os.DataHoraSolucao.Value.Date <= parameters.DataHoraSolucaoFim.Value.Date);

            if (parameters.DataInicioDispBB.HasValue && parameters.DataFimDispBB.HasValue)
                query = query.Where(os => os.DataHoraAberturaOS >= parameters.DataInicioDispBB
                    && os.DataHoraAberturaOS <= parameters.DataFimDispBB);

            if (!string.IsNullOrWhiteSpace(parameters.PAS))
            {
                int[] pas = parameters.PAS.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => pas.Contains(os.PA.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodPostos))
            {
                int[] postos = parameters.CodPostos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => postos.Contains(os.CodPosto.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.NotIn_CodStatusServicos))
            {
                int[] cods = parameters.NotIn_CodStatusServicos.Split(",").Select(a => int.Parse(a.Trim())).ToArray();
                query = query.Where(os => !cods.Contains(os.CodStatusServico.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodOS))
            {
                int[] cods = parameters.CodOS.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodOS.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodStatusServicos))
            {
                int[] cods = parameters.CodStatusServicos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodStatusServico.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodRegioes))
            {
                int[] cods = parameters.CodRegioes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodRegiao.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.NumSerie))
                query = query.Where(os => os.NumSerie.Trim().ToLower() == parameters.NumSerie.Trim().ToLower());

            if (!string.IsNullOrWhiteSpace(parameters.Defeito))
                query = query.Where(os => os.DefeitoRelatado.Contains(parameters.Defeito));

            if (!string.IsNullOrWhiteSpace(parameters.Solucao))
                query = query.Where(os => os.RelatoSolucao.Contains(parameters.Solucao));

            if (!string.IsNullOrWhiteSpace(parameters.CodTecnicos))
            {
                int[] cods = parameters.CodTecnicos.Split(",").Select(a => int.Parse(a.Trim())).Where(s => s > 0).Distinct().ToArray();

                if (parameters.Include == OrdemServicoIncludeEnum.OS_LISTA)
                    query = query.Where(os => (os.CodTecnico.HasValue && cods.Contains(os.CodTecnico.Value))
                        || cods.Contains(os.CodTecnico.Value));
                else
                    query = query.Where(os => os.CodTecnico.HasValue && cods.Contains(os.CodTecnico.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodTiposIntervencao))
            {
                int[] cods = parameters.CodTiposIntervencao.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodTipoIntervencao.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodCliente.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodEquipamentos))
            {
                int[] cods = parameters.CodEquipamentos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodEquip.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodFilial.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.CodAutorizadas))
            {
                int[] cods = parameters.CodAutorizadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodAutorizada.Value));
            }

            if (!string.IsNullOrWhiteSpace(parameters.PontosEstrategicos))
            {
                string[] cods = parameters.PontosEstrategicos.Split(",").Select(a => a.Trim()).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.PontoEstrategico));
            }

            return query;
        }
    }
}