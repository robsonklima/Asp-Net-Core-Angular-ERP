using System.Security.Claims;
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
    public class PecaStatusController : ControllerBase
    {
        private readonly IPecaStatusService _pecaStatusService;

        public PecaStatusController(IPecaStatusService pecaStatusService)
        {
            _pecaStatusService = pecaStatusService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PecaStatusParameters parameters)
        {
            return _pecaStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPecaStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PecaStatus Get(int codPecaStatus)
        {
            return _pecaStatusService.ObterPorCodigo(codPecaStatus);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] PecaStatus pecaStatus)
        {
            _pecaStatusService.Criar(pecaStatus);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PecaStatus pecaStatus)
        {
            _pecaStatusService.Atualizar(pecaStatus);
        }

        [HttpDelete("{codPecaStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPecaStatus)
        {
            _pecaStatusService.Deletar(codPecaStatus);
        }

        [HttpGet("export")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public IActionResult ExportToExcel([FromQuery] PecaStatusParameters parameters)
        {
            return _pecaStatusService.ExportToExcel(parameters);
        }
    }
}