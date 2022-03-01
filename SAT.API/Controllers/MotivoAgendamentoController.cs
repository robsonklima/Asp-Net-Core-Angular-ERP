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
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class MotivoAgendamentoController : ControllerBase
    {
        private readonly IMotivoAgendamentoService _motivoAgendamentoService;

        public MotivoAgendamentoController(IMotivoAgendamentoService motivoAgendamentoService)
        {
            _motivoAgendamentoService = motivoAgendamentoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] MotivoAgendamentoParameters parameters)
        {
            return _motivoAgendamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codMotivo}")]
        public MotivoAgendamento Get(int codMotivo)
        {
            return _motivoAgendamentoService.ObterPorCodigo(codMotivo);
        }

        [HttpPost]
        public void Post([FromBody] MotivoAgendamento motivoAgendamento)
        {
            _motivoAgendamentoService.Criar(motivoAgendamento);
        }

        // PUT api/<MotivoAgendamentoController>/5
        [HttpPut("{codMotivo}")]
        public void Put([FromBody] MotivoAgendamento motivoAgendamento)
        {
            _motivoAgendamentoService.Atualizar(motivoAgendamento);
        }

        // DELETE api/<MotivoAgendamentoController>/5
        [HttpDelete("{codMotivo}")]
        public void Delete(int codMotivo)
        {
            _motivoAgendamentoService.Deletar(codMotivo);
        }
    }
}
