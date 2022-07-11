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
    public class TicketAtendimentoController : ControllerBase
    {
        private readonly ITicketAtendimentoService _ticketAtendimentoService;

        public TicketAtendimentoController(
            ITicketAtendimentoService ticketAtendimentoService
        )
        {
            _ticketAtendimentoService = ticketAtendimentoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TicketAtendimentoParameters parameters)
        {
            return _ticketAtendimentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTicketAtendimento}")]
        public TicketAtendimento Get(int codTicketAtendimento)
        {
            return _ticketAtendimentoService.ObterPorCodigo(codTicketAtendimento);
        }

        // [HttpPost]
        // public EquipamentoContrato Post([FromBody] EquipamentoContrato equipamentoContrato)
        // {
        //     return _equipamentoContratoService.Criar(equipamentoContrato);
        // }

        // [HttpPut]
        // public void Put([FromBody] EquipamentoContrato equipamentoContrato)
        // {
        //     _equipamentoContratoService.Atualizar(equipamentoContrato);
        // }

        // [HttpDelete("{codEquipContrato}")]
        // public void Delete(int codEquipContrato)
        // {
        //     _equipamentoContratoService.Deletar(codEquipContrato);
        // }
    }
}
