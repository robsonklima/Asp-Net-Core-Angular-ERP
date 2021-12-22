using SAT.MODELS.Entities;
using System.Linq;
using System;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Enums;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarFiltroPadrao(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Filter))
                query = query.Where(t =>
                    t.CodOS.ToString().Contains(parameters.Filter) ||
                    t.Cliente.NumBanco.Contains(parameters.Filter) ||
                    t.Cliente.NomeFantasia.Contains(parameters.Filter) ||
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
                query = query
                    .Where(os => os.RelatoriosAtendimento.Any(r => r.DataHoraInicio.Date >= parameters.DataHoraInicioInicio.Value.Date &&
                    r.DataHoraInicio.Date <= parameters.DataHoraInicioFim.Value.Date));

            if (!string.IsNullOrEmpty(parameters.NumOSCliente))
                query = query.Where(os => os.NumOSCliente == parameters.NumOSCliente);

            if (!string.IsNullOrEmpty(parameters.NumOSQuarteirizada))
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
                query = query.Where(os => os.RelatoriosAtendimento.Any(r => r.DataHoraSolucao.Date >= parameters.DataHoraSolucaoInicio.Value.Date &&
                   r.DataHoraSolucao.Date <= parameters.DataHoraSolucaoFim.Value.Date));

            if (parameters.DataInicioDispBB.HasValue && parameters.DataFimDispBB.HasValue)
                query = query.Where(os => os.DataHoraAberturaOS >= parameters.DataInicioDispBB
                    && os.DataHoraAberturaOS <= parameters.DataFimDispBB);

            if (!string.IsNullOrEmpty(parameters.PAS))
            {
                int[] pas = parameters.PAS.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => pas.Contains(os.RegiaoAutorizada.PA.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodTiposGrupo))
            {
                query = query.Where(os => os.EquipamentoContrato != null
                    && parameters.CodTiposGrupo.Contains(os.EquipamentoContrato.CodTipoEquip.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(parameters.NotIn_CodStatusServicos))
            {
                int[] cods = parameters.NotIn_CodStatusServicos.Split(",").Select(a => int.Parse(a.Trim())).ToArray();
                query = query.Where(os => !cods.Contains(os.CodStatusServico));
            }

            if (!string.IsNullOrEmpty(parameters.CodOS))
            {
                int[] cods = parameters.CodOS.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodOS));
            }

            if (!string.IsNullOrEmpty(parameters.CodStatusServicos))
            {
                int[] cods = parameters.CodStatusServicos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodStatusServico));
            }

            if (!string.IsNullOrEmpty(parameters.CodRegioes))
            {
                int[] cods = parameters.CodRegioes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.RegiaoAutorizada.CodRegiao.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodTecnicos))
            {
                int[] cods = parameters.CodTecnicos.Split(",").Select(a => int.Parse(a.Trim())).Where(s => s > 0).Distinct().ToArray();

                if (parameters.Include == OrdemServicoIncludeEnum.OS_LISTA)
                    query = query.Where(os => (os.CodTecnico.HasValue && cods.Contains(os.CodTecnico.Value))
                        || cods.Contains(os.RelatoriosAtendimento.FirstOrDefault().CodTecnico));
                else
                    query = query.Where(os => os.CodTecnico.HasValue && cods.Contains(os.CodTecnico.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodTiposIntervencao))
            {
                int[] cods = parameters.CodTiposIntervencao.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.TipoIntervencao.CodTipoIntervencao.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodClientes))
            {
                int[] cods = parameters.CodClientes.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodCliente));
            }

            if (!string.IsNullOrEmpty(parameters.CodEquipamentos))
            {
                int[] cods = parameters.CodEquipamentos.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodEquip.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                int[] cods = parameters.CodFiliais.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodFilial.Value));
            }

            if (!string.IsNullOrEmpty(parameters.CodAutorizadas))
            {
                int[] cods = parameters.CodAutorizadas.Split(",").Select(a => int.Parse(a.Trim())).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.CodAutorizada.Value));
            }

            if (!string.IsNullOrEmpty(parameters.PontosEstrategicos))
            {
                string[] cods = parameters.PontosEstrategicos.Split(",").Select(a => a.Trim()).Distinct().ToArray();
                query = query.Where(os => cods.Contains(os.EquipamentoContrato.PontoEstrategico));
            }

            return query;
        }
    }
}