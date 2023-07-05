using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using System.Linq;
using SAT.MODELS.Enums;
using System;
using SAT.MODELS.Entities.Constants;

namespace SAT.INFRA.Repository
{
    public partial class OrdemServicoRepository : IOrdemServicoRepository
    {
        public IQueryable<OrdemServico> AplicarFiltroAgendaTecnico(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                int[] filiais = parameters.CodFiliais.Split(',').Select(f => int.Parse(f.Trim())).Distinct().ToArray();
                query = query.Where(os => filiais.Contains(os.CodFilial.Value));
            }

            if (parameters.CodTecnico.HasValue)
                return query.Where(os => os.CodTecnico == parameters.CodTecnico
                    && os.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO);

            query = query.Where(os =>
                (os.CodStatusServico == (int)StatusServicoEnum.TRANSFERIDO ||
                (os.CodStatusServico == (int)StatusServicoEnum.FECHADO && os.DataHoraFechamento.Value.Date >= parameters.InicioPeriodoAgenda.Value.Date && os.DataHoraFechamento.Value.Date <= parameters.FimPeriodoAgenda.Value.Date)));

            return query;
        }

        public IQueryable<OrdemServico> AplicarFiltroIndicadores(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            if (parameters.DataAberturaInicio != DateTime.MinValue && parameters.DataAberturaFim != DateTime.MinValue)
            {
                query = query.Where(os => os.DataHoraAberturaOS >= parameters.DataAberturaInicio
                    && os.DataHoraAberturaOS <= parameters.DataAberturaFim);
            }

            if (parameters.DataFechamentoInicio.HasValue && parameters.DataFechamentoFim.HasValue)
            {
                query = query.Where(os => os.DataHoraFechamento >= parameters.DataFechamentoInicio
                    && os.DataHoraFechamento <= parameters.DataFechamentoFim);
            }

            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                int[] filiais = parameters.CodFiliais.Split(',').Select(f => int.Parse(f.Trim())).Distinct().ToArray();
                query = query.Where(os => filiais.Contains(os.CodFilial.Value));
            }

            return query;
        }

        public IQueryable<OrdemServico> AplicarFiltroChamadosMaisAntigos(IQueryable<OrdemServico> query, OrdemServicoParameters parameters, TipoIntervencaoEnum tipoIntervencao)
        {
            int[] codServicosExclusos = new int[] { 2, 3 };

            query = query
                .Where(s => s.CodTipoIntervencao == (int)tipoIntervencao/*2: corretivo / 17: orcamento aprovado*/ && !codServicosExclusos.Contains(s.CodStatusServico)
                && s.Filial.IndAtivo == 1
                && s.Filial.CodFilial != 21/*PERTO - OUTSOURCING*/
                && !s.Equipamento.NomeEquip.Contains("POS")
                && !s.Equipamento.NomeEquip.Contains("PIN")
                && !s.Equipamento.NomeEquip.Contains("PERTOS")
                );

            return query;
        }

        public IQueryable<OrdemServico> AplicarFiltroPecasFaltantesFiliais(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            return query.Where(os =>
                            os.CodStatusServico == (int)StatusServicoEnum.PECA_FALTANTE &&
                            (os.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA ||
                            os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORCAMENTO ||
                            os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_APROVADO ||
                            os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_REPROVADO ||
                            os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE ||
                            os.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO)
                            );
        }

        public IQueryable<OrdemServico> AplicarFiltroDisponiblidadeBBCalculaOS(IQueryable<OrdemServico> query)
        {

            DateTime primeiroDiaMes = new(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime ultimoDiaMes = primeiroDiaMes.AddMonths(1);
            string anoMes = DateTime.Now.ToString("yyyyMM");

            var po2 = query.Where(c => c.CodCliente == (int)ClienteEnum.BB &&
                                       c.IndServico == 1 &&
                                       c.CodUsuarioCadastro == "SERVICO" &&
                                       c.DispBBEquipamentoContrato != null &&
                                       c.DispBBEquipamentoContrato.CodContrato == 3145 &&
                                       c.Equipamento != null &&
                                       c.DispBBEquipamentoContrato.AnoMes == anoMes
                                                                &&
                                                                (c.CodTipoIntervencao == (int)TipoIntervencaoEnum.CORRETIVA ||
                                                                c.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORCAMENTO ||
                                                                c.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_APROVADO ||
                                                                c.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_REPROVADO ||
                                                                c.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_APROVACAO_CLIENTE ||
                                                                c.CodTipoIntervencao == (int)TipoIntervencaoEnum.ORC_PEND_FILIAL_DETALHAR_MOTIVO
                                                                )

                                     && !c.DispBBEquipamentoContrato.Equipamento.NomeEquip.Contains("TDS")
                                     && !c.DispBBEquipamentoContrato.Equipamento.NomeEquip.Contains("TCC")
                                     && !c.DispBBEquipamentoContrato.Equipamento.NomeEquip.Contains("TOP")
                                     && !c.DispBBEquipamentoContrato.Equipamento.NomeEquip.Contains("TR 1150")

                                     ).AsEnumerable();

            var po = //po2.Where(c => (
                            from c in po2

                            let proxDiaUtil = this.DispBBRetornaProximoDiaUtil(c.DispBBEquipamentoContrato, c.DataHoraAberturaOS.Value)
                            let ultimaRat = c.RelatoriosAtendimento.OrderByDescending(or => or.CodRAT).FirstOrDefault()

                            // &&
                            // Considera chamados abertos no periodo.
                            where (c.DataHoraAberturaOS >= primeiroDiaMes && c.DataHoraAberturaOS <= ultimoDiaMes)
                                   ||

                                   //Considera data da proxima janela no periodo(Antiga regra de agendamento).
                                   (proxDiaUtil >= primeiroDiaMes && proxDiaUtil <= ultimoDiaMes
                                    && (ultimaRat == null || (ultimaRat != null && ultimaRat.DataHoraSolucao > proxDiaUtil))
                                   )
                                   ||

                                   //Considera chamados fechados no per�odo.
                                   (c.RelatoriosAtendimento.Any(s => s.DataHoraSolucao >= primeiroDiaMes && s.DataHoraSolucao <= ultimoDiaMes))
                                    ||

                                   //Considera chamados abertos em data anterior ao per�odo e ainda n�o fechado.
                                   (c.CodStatusServico != (int)StatusServicoEnum.CANCELADO &&
                                       c.CodStatusServico != (int)StatusServicoEnum.FECHADO &&
                                       c.DataHoraAberturaOS < primeiroDiaMes)
                                    ||

                                   (c.DataHoraAberturaOS < primeiroDiaMes && (ultimaRat != null && ultimaRat.DataHoraSolucao > ultimoDiaMes))

                            select c;

            return po.AsQueryable();
        }

        private DateTime DispBBRetornaProximoDiaUtil(DispBBEquipamentoContrato equipamentoContrato, DateTime DataHoraAbertura)
        {
            DateTime retorno = DateTime.Now;

            AcordoNivelServico dadosSLA = equipamentoContrato.AcordoNivelServico;
            bool? indSabado = dadosSLA.IndSabado;
            bool? indDomingo = dadosSLA.IndDomingo;
            bool? indFeriado = dadosSLA.IndFeriado;
            DateTime? horarioInicio = dadosSLA.HorarioInicio;

            if ((equipamentoContrato.IndSEMAT == 1 && dadosSLA.CodSLA == 155) || (dadosSLA.CodSLA == 153 && dadosSLA.CodSLA == 154))
            {
                retorno = DataHoraAbertura.AddDays(1).Date.Add(
                    new TimeSpan(horarioInicio.Value.Hour, horarioInicio.Value.Minute, horarioInicio.Value.Second));
            }
            else
            {
                retorno = DataHoraAbertura;
            }
            
            return retorno;
        }

        public IQueryable<OrdemServico> AplicarFiltroGenericText(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            if (!string.IsNullOrEmpty(parameters.CodFiliais))
            {
                var filiais = parameters.CodFiliais.Split(',').Select(a => a.Trim()).ToArray();
                query = query.Where(os => filiais.Any(f => f == os.CodFilial.ToString()));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Filter))
            {
                int parsedValue;
                Int32.TryParse(parameters.Filter, out parsedValue);

                if (parsedValue != 0)
                    return query.Where(t => t.CodOS == parsedValue);

                query = query.Where(t =>
                    t.Cliente.NumBanco.Contains(parameters.Filter) ||
                    t.Cliente.NomeFantasia.Contains(parameters.Filter) ||
                    t.Equipamento.NomeEquip.Contains(parameters.Filter) ||
                    t.NumOSCliente.Contains(parameters.Filter));
            }

            return query;
        }

        public IQueryable<OrdemServico> AplicarFiltroMonitoramentoClientesExcetoBB(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            return query.Where(os => os.DataHoraAberturaOS >= DateTime.Now.AddDays(-30) &&
               os.CodCliente != Constants.CLIENTE_BB && (os.IndServico == 1 || os.IndIntegracao == 1 || os.CodUsuarioCad == "INTEGRACAO" || os.CodUsuarioCad == "INTEGRACAO-SAT")
               && (!os.Equipamento.NomeEquip.Contains("POS") && !os.Equipamento.NomeEquip.Contains("PERTOS") && os.CodCliente != Constants.SICOOB && os.CodCliente != Constants.SICOOB_CONFEDERACAO))
                .OrderByDescending(ord => ord.DataHoraAberturaOS);
        }

        public IQueryable<OrdemServico> AplicarFiltroMonitoramentoBB(IQueryable<OrdemServico> query, OrdemServicoParameters parameters)
        {
            return query.Where(os => os.DataHoraAberturaOS >= DateTime.Now.AddDays(-30) && os.CodCliente == Constants.CLIENTE_BB && os.EquipamentoContrato.IndAtivo == 1).OrderByDescending(g => g.DataHoraAberturaOS);
        }
    }
}