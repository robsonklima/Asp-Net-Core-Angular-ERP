using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class TicketClassificacaoController : ControllerBase
    {
        private readonly ITicketClassificacaoService _ticketClassificacaoService;

        public TicketClassificacaoController(
            ITicketClassificacaoService ticketClassificacaoService
        )
        {
            _ticketClassificacaoService = ticketClassificacaoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TicketClassificacaoParameters parameters)
        {
            return _ticketClassificacaoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codClassificacao}")]
        public TicketClassificacao Get(int codClassificacao)
        {
            return _ticketClassificacaoService.ObterPorCodigo(codClassificacao);
        }

        // [HttpPost]
        // public EquipamentoContrato Post([FromBody] EquipamentoContrato equipamentoContrato)
        // {
        //     return _equipamentoContratoService.Criar(equipamentoContrato);
        // }

        [HttpPut]
        public void Put([FromBody] TicketClassificacao ticketClassificacao)
        {
            _ticketClassificacaoService.Atualizar(ticketClassificacao);
        }

        // [HttpDelete("{codEquipContrato}")]
        // public void Delete(int codEquipContrato)
        // {
        //     _equipamentoContratoService.Deletar(codEquipContrato);
        // }
    }
}
