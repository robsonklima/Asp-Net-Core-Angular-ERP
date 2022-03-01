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
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoEquipamentoController : ControllerBase
    {
        private readonly ITipoEquipamentoService _tipoEquipamentoService;

        public TipoEquipamentoController(ITipoEquipamentoService tipoEquipamentoService)
        {
            _tipoEquipamentoService = tipoEquipamentoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] TipoEquipamentoParameters parameters)
        {
            return _tipoEquipamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoEquip}")]
        public TipoEquipamento Get(int codTipoEquip)
        {
            return _tipoEquipamentoService.ObterPorCodigo(codTipoEquip);
        }

        [HttpPost]
        public void Post([FromBody] TipoEquipamento tipoEquipamento)
        {
            _tipoEquipamentoService.Criar(tipoEquipamento: tipoEquipamento);
        }

        [HttpPut]
        public void Put([FromBody] TipoEquipamento tipoEquipamento)
        {
            _tipoEquipamentoService.Atualizar(tipoEquipamento);
        }

        [HttpDelete("{codTipoEquip}")]
        public void Delete(int codTipoEquip)
        {
            _tipoEquipamentoService.Deletar(codTipoEquip);
        }
    }
}
