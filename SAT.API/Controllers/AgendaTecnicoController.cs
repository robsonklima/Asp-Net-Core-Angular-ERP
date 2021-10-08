using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaTecnicoController : ControllerBase
    {
        private readonly IAgendaTecnicoService _agendaServ;

        public AgendaTecnicoController(IAgendaTecnicoService agendaServ)
        {
            _agendaServ = agendaServ;
        }

        [HttpGet]
        public List<AgendaTecnico> Get([FromQuery] AgendaTecnicoParameters parameters)
        {
            return _agendaServ.ObterPorParametros(parameters);
        }

        [HttpPost]
        public void Post([FromBody] AgendaTecnico evento)
        {
            _agendaServ.Criar(evento);
        }

        [HttpPut("{codAgendaTecnico}")]
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
