using System.Linq;
using SAT.MODELS.Entities.Params;

namespace SAT.SERVICES.Services {
    public partial class ExportacaoService
    {
        protected void GerarPlanilhaTicket(TicketParameters parameters)
		{
            var tickets = _ticketRepo.ObterPorParametros(parameters);
            var sheet = tickets.Select(t =>
                            new
                            {
                                Codigo = t.CodTicket,
                                Cadastro = t.DataHoraCad,
                                Solicitante = t.UsuarioCad?.NomeUsuario,
                                Atendente = t.UsuarioManut?.NomeUsuario,
                                Modulo = t.TicketModulo?.Descricao,
                                Prioridade = t.TicketPrioridade?.Descricao,
                                DataFechamento = t.DataHoraFechamento,
                                Status = t.TicketStatus.Descricao
                            });

            var wsOs = Workbook.Worksheets.Add("creditos");
			wsOs.Cell(2, 1).Value = sheet;
			WriteHeaders(sheet.FirstOrDefault(), wsOs);
        }
    }
}