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
            {
                query = query.Where(os => os.CodContrato == parameters.CodContrato.Value);
            }

            if (parameters.IndServico.HasValue)
            {
                query = query.Where(os => os.IndServico == parameters.IndServico.Value);
            }

            if (parameters.DataHoraInicioInicio != DateTime.MinValue && parameters.DataHoraInicioFim != DateTime.MinValue)
            {
                query = query
                    .Where(os => os.RelatoriosAtendimento
                    .Any(r => r.DataHoraInicio >= parameters.DataHoraInicioInicio && r.DataHoraInicio <= parameters.DataHoraInicioFim));
            }

            if (!string.IsNullOrEmpty(parameters.NumOSCliente))
                query = query.Where(os => os.NumOSCliente == parameters.NumOSCliente);

            if (!string.IsNullOrEmpty(parameters.NumOSQuarteirizada))
                query = query.Where(os => os.NumOSQuarteirizada == parameters.NumOSQuarteirizada);

            if (parameters.CodTecnico.HasValue)
            {
                query = query.Where(os => os.CodTecnico == parameters.CodTecnico);
            }

            if (parameters.CodEquipContrato.HasValue)
                query = query.Where(os => os.CodEquipContrato == parameters.CodEquipContrato);

            if (parameters.DataAberturaInicio != DateTime.MinValue && parameters.DataAberturaFim != DateTime.MinValue)
                query = query.Where(os => os.DataHoraAberturaOS >= parameters.DataAberturaInicio
                    && os.DataHoraAberturaOS <= parameters.DataAberturaFim);

            if (parameters.DataFechamentoInicio != DateTime.MinValue && parameters.DataFechamentoFim != DateTime.MinValue)
                query = query.Where(os => os.DataHoraFechamento >= parameters.DataFechamentoInicio
                    && os.DataHoraFechamento <= parameters.DataFechamentoFim);

            if (parameters.DataTransfInicio != DateTime.MinValue && parameters.DataTransfFim != DateTime.MinValue)
                query = query.Where(os => os.DataHoraTransf >= parameters.DataTransfInicio
                    && os.DataHoraTransf <= parameters.DataTransfFim);

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