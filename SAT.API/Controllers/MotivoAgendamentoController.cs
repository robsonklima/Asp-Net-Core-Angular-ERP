using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class MotivoAgendamentoController : ControllerBase
    {
        private readonly IMotivoAgendamentoRepository _motivoAgendamentoInterface;

        public MotivoAgendamentoController(IMotivoAgendamentoRepository motivoAgendamentoInterface)
        {
            _motivoAgendamentoInterface = motivoAgendamentoInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] MotivoAgendamentoParameters parameters)
        {
            var motivos = _motivoAgendamentoInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = motivos,
                TotalCount = motivos.TotalCount,
                CurrentPage = motivos.CurrentPage,
                PageSize = motivos.PageSize,
                TotalPages = motivos.TotalPages,
                HasNext = motivos.HasNext,
                HasPrevious = motivos.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codMotivo}")]
        public MotivoAgendamento Get(int codMotivo)
        {
            return _motivoAgendamentoInterface.ObterPorCodigo(codMotivo);
        }

        [HttpPost]
        public void Post([FromBody] MotivoAgendamento motivoAgendamento)
        {
            _motivoAgendamentoInterface.Criar(motivoAgendamento);
        }

        // PUT api/<MotivoAgendamentoController>/5
        [HttpPut("{codMotivo}")]
        public void Put([FromBody] MotivoAgendamento motivoAgendamento)
        {
            _motivoAgendamentoInterface.Atualizar(motivoAgendamento);
        }

        // DELETE api/<MotivoAgendamentoController>/5
        [HttpDelete("{codMotivo}")]
        public void Delete(int codMotivo)
        {
            _motivoAgendamentoInterface.Deletar(codMotivo);
        }
    }
}
