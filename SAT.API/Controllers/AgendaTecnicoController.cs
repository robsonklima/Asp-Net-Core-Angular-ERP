using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
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

        [HttpGet("Agenda")]
        public AgendaTecnico[] GetAgendaTecnico([FromQuery] AgendaTecnicoParameters parameters)
        {
            return _agendaServ.ObterAgendaTecnico(parameters);
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

        [HttpGet("CriarOS/{codOS},{codTecnico}")]
        public AgendaTecnico CriarAgendaTecnico(int codOS, int codTecnico)
        {
            return _agendaServ.CriarAgendaTecnico(codOS, codTecnico);
        }

        [HttpGet("DeletarOS/{codOS},{codTecnico}")]
        public void DeletarAgendaTecnico(int codOS, int codTecnico)
        {
            _agendaServ.DeletarAgendaTecnico(codOS, codTecnico);
        }


        [HttpPut]
        public AgendaTecnico Put([FromBody] AgendaTecnico evento)
        {
            return _agendaServ.Atualizar(evento);
        }

        [HttpDelete("{codAgendaTecnico}")]
        public void Delete(int codAgendaTecnico)
        {
            _agendaServ.Deletar(codAgendaTecnico);
        }
    }
}