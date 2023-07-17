using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.MODELS.Entities.Params;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

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
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] GrupoEquipamentoParameters parameters)
        {
            return _grupoEquipamentoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codGrupoEquip}/{codTipoEquip}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public GrupoEquipamento Get(int codGrupoEquip, int codTipoEquip)
        {
            return _grupoEquipamentoService.ObterPorCodigo(codGrupoEquip, codTipoEquip);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] GrupoEquipamento grupoEquipamento)
        {
            _grupoEquipamentoService.Criar(grupoEquipamento);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] GrupoEquipamento grupoEquipamento)
        {
            _grupoEquipamentoService.Atualizar(grupoEquipamento);
        }

        [HttpDelete("{codGrupoEquip}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codGrupoEquip)
        {
            _grupoEquipamentoService.Deletar(codGrupoEquip);
        }
    }
}
