using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class AgendaTecnicoController : ControllerBase
    {
        private readonly IAgendaTecnicoService _agendaServ;

        public AgendaTecnicoController(IAgendaTecnicoService agendaServ)
        {
            _agendaServ = agendaServ;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] AgendaTecnicoParameters parameters)
        {
            return _agendaServ.ObterPorParametros(parameters);
        }

        [HttpGet("{codAgendaTecnico}")]
        public AgendaTecnico Get(int codAgendaTecnico)
        {
            return _agendaServ.ObterPorCodigo(codAgendaTecnico);
        }

        [HttpPost]
        public AgendaTecnico Post([FromBody] AgendaTecnico evento)
        {
            _agendaServ.Criar(evento);
            return evento;
        }

        [HttpPut]
        public void Put([FromBody] AgendaTecnico evento)
        {
            _agendaServ.Atualizar(evento);
        }

        [HttpDelete("{codAgendaTecnico}")]
        public void Delete(int codAgendaTecnico)
        {
            _agendaServ.Deletar(codAgendaTecnico);
        }
    }
}