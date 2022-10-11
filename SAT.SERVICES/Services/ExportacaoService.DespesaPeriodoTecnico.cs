using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaDespesaPeriodoTecnico(DespesaPeriodoTecnicoParameters parameters)
		{
            var despesas = _despesaPeriodoTecnicoRepo.ObterPorParametros(parameters);

            var sheet = despesas.Select(d => new {
                CodDespesaPeriodoTecnico = d.CodDespesaPeriodoTecnico,
                Protocolo = d.DespesaProtocoloPeriodoTecnico?.CodDespesaProtocolo,
                Tecnico = d.Tecnico?.Nome,
                Status = d.DespesaPeriodoTecnicoStatus?.NomeDespesaPeriodoTecnicoStatus,
                InicioPeriodo = d.DespesaPeriodo.DataInicio,
                FimPeriodo = d.DespesaPeriodo.DataFim,
                ProcessadoJuntoATicket = d.TicketLogPedidoCredito?.IndProcessado == 1 ? "SIM" : "NÃO",
                DataHoraProcessamentoTicket = d.TicketLogPedidoCredito?.DataHoraProcessamento,
                NumeroCartao = d.TicketLogPedidoCredito?.NumeroCartao,
                Observacao = d.TicketLogPedidoCredito?.Observacao,
                Creditado = d.IndCredito == 1 ? "SIM" : "NÃO",
                DataHoraCredito = d.DataHoraCredito,
                UsuarioCredito = d.CodUsuarioCredito,
                Compensado = d.IndCompensacao == 1 ? "SIM" : "NÃO",
                DataHoraCompensacao = d.DataHoraCompensacao,
                UsuarioCompensacao = d.CodUsuarioCompensacao,
                Verificado = d.IndVerificacao == 1 ? "SIM" : "NÃO",
                DataHoraVerificacao = d.DataHoraVerificacao,
                UsuarioVerificacao = d.CodUsuarioVerificacao,
            });

            var wsOs = Workbook.Worksheets.Add("despesas");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}