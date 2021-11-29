using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class ContratoEquipamentoController : ControllerBase
    {
        private readonly IContratoEquipamentoService _contratoEquipamentoInterface;

        public ContratoEquipamentoController(IContratoEquipamentoService contratoEquipamentoInterface)
        {
            _contratoEquipamentoInterface = contratoEquipamentoInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ContratoEquipamentoParameters parameters)
        {
            return _contratoEquipamentoInterface.ObterPorParametros(parameters);
        }


        [HttpGet("{codContratoEquipamento}/{codEquip}")]
        public ContratoEquipamento Get(int codContratoEquipamento, int codEquip)
        {
            return _contratoEquipamentoInterface.ObterPorCodigo(codContratoEquipamento, codEquip);
        }

        [HttpPost]
        public void Post([FromBody] ContratoEquipamento contratoEquipamento)
        {
            _contratoEquipamentoInterface.Criar(contratoEquipamento);
        }

        [HttpPut]
        public void Put([FromBody] ContratoEquipamento contratoEquipamento)
        {
            _contratoEquipamentoInterface.Atualizar(contratoEquipamento);
        }

        [HttpDelete("{codContratoEquipamento}")]
        public void Delete(int codContratoEquipamento)
        {
            throw new System.NotImplementedException("DELETAR NÃO IMPLEMENTADO");
        }
    }
}
