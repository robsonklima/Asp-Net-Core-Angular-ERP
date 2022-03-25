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
    public class EquipamentoController : ControllerBase
    {
        private readonly IEquipamentoService _equipamentoService;

        public EquipamentoController(IEquipamentoService equipamentoService)
        {
            _equipamentoService = equipamentoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] EquipamentoParameters parameters)
        {
            return _equipamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codEquip}")]
        public Equipamento Get(int codEquip)
        {
            return _equipamentoService.ObterPorCodigo(codEquip);
        }

        [HttpPost]
        public void Post([FromBody] Equipamento equipamento)
        {
            this._equipamentoService.Criar(equipamento);
        }

        [HttpPut]
        public void Put([FromBody] Equipamento equipamento)
        {
            this._equipamentoService.Atualizar(equipamento);
        }

        [HttpDelete("{codEquip}")]
        public void Delete(int codEquip)
        {
        }
    }
}
