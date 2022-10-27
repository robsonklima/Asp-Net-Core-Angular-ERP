using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaTicketLogPedidoCredito(TicketLogPedidoCreditoParameters parameters)
		{
            var clientes = _ticketLogPedidoCreditoRepo.ObterPorParametros(parameters);
            var sheet = clientes.Select(t =>
                            new
                            {
                                Codigo = t.CodTicketLogPedidoCredito
                            });

            var wsOs = Workbook.Worksheets.Add("creditos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}