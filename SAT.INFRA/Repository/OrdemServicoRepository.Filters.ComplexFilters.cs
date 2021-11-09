using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using System.Linq;
using SAT.MODELS.Enums;
using System;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarFiltroAgendaTecnico(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(',').Select(f => f.Trim());
                query = query.Where(os => filiais.Any(p => p == os.CodFilial.ToString()));
            }

            query = query.Where(os =>
                // (os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA || os.CodTipoIntervencao == (int)TipoIntervencaoEnum.INSTALACAO) && 
                (os.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO ||
                ((os.CodStatusServico == (int)StatusServicoEnum.ABERTO || os.CodStatusServico == (int)StatusServicoEnum.FECHADO) && os.DataHoraTransf.Value.Date == DateTime.Now.Date)));

            return query;
        }

        public IQueryable<OrdemServico> AplicarFiltroIndicadores(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            if (parameters.DataAberturaInicio != DateTime.MinValue && parameters.DataAberturaFim != DateTime.MinValue)
            {
                query = query.Where(os => os.DataHoraAberturaOS >= parameters.DataAberturaInicio
                    && os.DataHoraAberturaOS <= parameters.DataAberturaFim);
            }

            if (parameters.DataFechamentoInicio != DateTime.MinValue && parameters.DataFechamentoFim != DateTime.MinValue)
            {
                query = query.Where(os => os.DataHoraFechamento >= parameters.DataFechamentoInicio
                    && os.DataHoraFechamento <= parameters.DataFechamentoFim);
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(',').Select(f => f.Trim());
                query = query.Where(os => filiais.Any(p => p == os.CodFilial.ToString()));
            }

            return query;
        }

        public IQueryable<OrdemServico> AplicarFiltroChamadosMaisAntigos(IQueryable<OrdemServico> query, OrdemServicoParameters parameters, TipoIntervencaoEnum tipoIntervencao)
        {
            int[] codServicosExclusos = new int[] { 2, 3 };

            query = query
                .Where(s => s.CodTipoIntervencao == (int)tipoIntervencao/*2: corretivo / 17: orçamento aprovado*/ && !codServicosExclusos.Contains(s.CodStatusServico)
                && s.Filial.IndAtivo == 1
                && s.Filial.CodFilial != 21/*PERTO - OUTSOURCING*/
                && !s.Equipamento.NomeEquip.Contains("POS")
                && !s.Equipamento.NomeEquip.Contains("PIN")
                && !s.Equipamento.NomeEquip.Contains("PERTOS")
                );

            return query;
        }
    }
}
