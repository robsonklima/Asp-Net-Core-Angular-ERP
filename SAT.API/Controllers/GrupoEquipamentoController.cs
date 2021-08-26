using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class GrupoEquipamentoController : ControllerBase
    {
        private IGrupoEquipamentoService _grupoEquipamentoService;

        public GrupoEquipamentoController(
            IGrupoEquipamentoService grupoEquipamentoService
        )
        {
            _grupoEquipamentoService = grupoEquipamentoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] GrupoEquipamentoParameters parameters)
        {
            return _grupoEquipamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codGrupoEquip}")]
        public GrupoEquipamento Get(int codGrupoEquip)
        {
            return _grupoEquipamentoService.ObterPorCodigo(codGrupoEquip);
        }

        [HttpPost]
        public void Post([FromBody] GrupoEquipamento grupoEquipamento)
        {
            
        }

        [HttpPut]
        public void Put([FromBody] GrupoEquipamento grupoEquipamento)
        {
            _grupoEquipamentoService.Atualizar(grupoEquipamento);
        }

        [HttpDelete("{codGrupoEquip}")]
        public void Delete(int codGrupoEquip)
        {
            _grupoEquipamentoService.Deletar(codGrupoEquip);
        }
    }
}
