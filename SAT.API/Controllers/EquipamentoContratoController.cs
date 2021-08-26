using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoContratoController : ControllerBase
    {
        private readonly IEquipamentoContratoService _equipamentoContratoService;

        public EquipamentoContratoController(
            IEquipamentoContratoService equipamentoContratoService
        )
        {
            _equipamentoContratoService = equipamentoContratoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] EquipamentoContratoParameters parameters)
        {
            return _equipamentoContratoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codEquipContrato}")]
        public EquipamentoContrato Get(int codEquipContrato)
        {
            return _equipamentoContratoService.ObterPorCodigo(codEquipContrato);
        }

        [HttpPost]
        public EquipamentoContrato Post([FromBody] EquipamentoContrato equipamentoContrato)
        {
            return _equipamentoContratoService.Criar(equipamentoContrato);
        }

        [HttpPut]
        public void Put([FromBody] EquipamentoContrato equipamentoContrato)
        {
            _equipamentoContratoService.Atualizar(equipamentoContrato);
        }

        [HttpDelete("{codEquipContrato}")]
        public void Delete(int codEquipContrato)
        {
            _equipamentoContratoService.Deletar(codEquipContrato);
        }
    }
}
