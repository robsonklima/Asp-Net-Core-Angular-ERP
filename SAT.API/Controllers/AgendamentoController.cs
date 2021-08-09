using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.API.Repositories.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.Entities.Constants;
using SAT.MODELS.ViewModels;
using System.Collections.Generic;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamentoRepository _agendamentoInterface;
        private readonly ISequenciaRepository _sequenciaInterface;

        public AgendamentoController(
            IAgendamentoRepository agendamentoInterface,
            ISequenciaRepository sequenciaInterface
        )
        {
            _agendamentoInterface = agendamentoInterface;
            _sequenciaInterface = sequenciaInterface;
        }

        [HttpGet]
        public AgendamentoListViewModel Get([FromQuery] AgendamentoParameters parameters)
        {
            var agendamentos = _agendamentoInterface.ObterPorParametros(parameters);

            var lista = new AgendamentoListViewModel
            {
                Agendamentos = agendamentos,
                TotalCount = agendamentos.TotalCount,
                CurrentPage = agendamentos.CurrentPage,
                PageSize = agendamentos.PageSize,
                TotalPages = agendamentos.TotalPages,
                HasNext = agendamentos.HasNext,
                HasPrevious = agendamentos.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codAgendamento}")]
        public Agendamento Get(int codAgendamento)
        {
            return _agendamentoInterface.ObterPorCodigo(codAgendamento);
        }

        [HttpPost]
        public void Post([FromBody] Agendamento agendamento)
        {
            agendamento.CodAgendamento = _sequenciaInterface.ObterContador(Constants.TABELA_AGENDAMENTO);
            _agendamentoInterface.Criar(agendamento);
        }

        [HttpPut("{codAgendamento}")]
        public void Put(int codAgendamento, [FromBody] Agendamento agendamento)
        {
            _agendamentoInterface.Atualizar(agendamento);
        }

        [HttpDelete("{codAgendamento}")]
        public void Delete(int codAgendamento)
        {
            _agendamentoInterface.Deletar(codAgendamento);
        }
    }
}
