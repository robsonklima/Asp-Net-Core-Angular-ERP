using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Views;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
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

        [HttpGet("View")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<ViewAgendaTecnicoRecurso> GetView([FromQuery] AgendaTecnicoParameters parameters)
        {
            return _agendaServ.ObterViewPorParametros(parameters);
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public List<AgendaTecnico> Get([FromQuery] AgendaTecnicoParameters parameters)
        {
            return _agendaServ.ObterPorParametros(parameters);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public AgendaTecnico Post([FromBody] AgendaTecnico agenda)
        {
            _agendaServ.Criar(agenda);
            
            return agenda;
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public AgendaTecnico Put([FromBody] AgendaTecnico evento)
        {
            return _agendaServ.Atualizar(evento);
        }
    }
}