using Microsoft.AspNetCore.Mvc;
using SAT.INFRA.Interfaces;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities;
using Microsoft.AspNetCore.Authorization;


namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipamentoController : ControllerBase
    {
        private readonly IEquipamentoRepository _equipamentoInterface;

        public EquipamentoController(IEquipamentoRepository equipamentoInterface)
        {
            _equipamentoInterface = equipamentoInterface;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] EquipamentoParameters parameters)
        {
            var equipamentos = _equipamentoInterface.ObterPorParametros(parameters);

            var lista = new ListViewModel
            {
                Items = equipamentos,
                TotalCount = equipamentos.TotalCount,
                CurrentPage = equipamentos.CurrentPage,
                PageSize = equipamentos.PageSize,
                TotalPages = equipamentos.TotalPages,
                HasNext = equipamentos.HasNext,
                HasPrevious = equipamentos.HasPrevious
            };

            return lista;
        }

        [HttpGet("{codEquip}")]
        public Equipamento Get(int codEquip)
        {
            return _equipamentoInterface.ObterPorCodigo(codEquip);
        }

        [HttpPost]
        public void Post([FromBody] Equipamento equipamento)
        {
        }

        [HttpPut]
        public void Put([FromBody] Equipamento equipamento)
        {
        }

        [HttpDelete("{codEquip}")]
        public void Delete(int codEquip)
        {
        }
    }
}
