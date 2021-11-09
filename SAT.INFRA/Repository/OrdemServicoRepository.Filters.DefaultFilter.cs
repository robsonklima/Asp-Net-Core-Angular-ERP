using SAT.MODELS.Entities;
using System.Linq;
using System;
using SAT.INFRA.Interfaces;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarFiltroPadrao(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.Filter))
            {
                query = query.Where(
                    t =>
                    t.CodOS.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Cliente.NumBanco.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Cliente.NomeFantasia.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }
            
            if (parameters.CodOS > 0) {
                query = query.Where(os => os.CodOS == parameters.CodOS);
            }

            if (!string.IsNullOrEmpty(parameters.NumOSCliente))
                query = query.Where(os => os.NumOSCliente == parameters.NumOSCliente);

            if (!string.IsNullOrEmpty(parameters.NumOSQuarteirizada))
                query = query.Where(os => os.NumOSQuarteirizada == parameters.NumOSQuarteirizada);

            if (parameters.CodTecnico.HasValue)
                query = query.Where(os => os.CodTecnico == parameters.CodTecnico);

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

            if (!string.IsNullOrEmpty(parameters.Filter))
            {
                query = query.Where(
                    t =>
                    t.CodOS.ToString().Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Cliente.NumBanco.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty) ||
                    t.Cliente.NomeFantasia.Contains(!string.IsNullOrWhiteSpace(parameters.Filter) ? parameters.Filter : string.Empty)
                );
            }

            if (!string.IsNullOrEmpty(parameters.PAS))
            {
                var pas = parameters.PAS.Split(",").Select(a => a.Trim());
                query = query.Where(os => pas.Any(p => p == os.RegiaoAutorizada.PA.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodTiposGrupo))
                query = query.Where(os => os.EquipamentoContrato != null
                    && parameters.CodTiposGrupo.Contains(os.EquipamentoContrato.CodTipoEquip.ToString()));

            if (!string.IsNullOrEmpty(parameters.CodStatusServicos))
            {
                var statusServicos = parameters.CodStatusServicos.Split(",").Select(p => p.Trim());
                query = query.Where(os => statusServicos.Any(r => r == os.CodStatusServico.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodRegioes))
            {
                var regioes = parameters.CodRegioes.Split(",").Select(r => r.Trim());
                query = query.Where(os => regioes.Any(r => r == os.RegiaoAutorizada.CodRegiao.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodTecnicos))
            {
                var tecnicos = parameters.CodTecnicos.Split(",").Select(t => t.Trim());
                query = query.Where(os => tecnicos.Any(r => r == os.CodTecnico.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodTiposIntervencao))
            {
                var tiposIntervencao = parameters.CodTiposIntervencao.Split(",").Select(t => t.Trim());
                query = query.Where(os => tiposIntervencao.Any(r => r == os.TipoIntervencao.CodTipoIntervencao.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodClientes))
            {
                var clientes = parameters.CodClientes.Split(",").Select(c => c.Trim());
                query = query.Where(os => clientes.Any(r => r == os.CodCliente.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodEquipamentos))
            {
                var equipamentos = parameters.CodEquipamentos.Split(",").Select(e => e.Trim());
                query = query.Where(os => equipamentos.Any(r => r == os.CodEquip.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(',').Select(f => f.Trim());
                query = query.Where(os => filiais.Any(p => p == os.CodFilial.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.CodAutorizadas))
            {
                var autorizadas = parameters.CodAutorizadas.Split(",").Select(a => a.Trim());
                query = query.Where(os => autorizadas.Any(r => r == os.CodAutorizada.ToString()));
            }

            if (!string.IsNullOrEmpty(parameters.PontosEstrategicos))
            {
                var pontos = parameters.PontosEstrategicos.Split(',').Select(p => p.Trim()); ;
                query = query.Where(os => pontos.Any(p => p == os.EquipamentoContrato.PontoEstrategico));
            }

            return query;
        }
    }
}