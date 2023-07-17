using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;
using System.Security.Claims;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class PontoPeriodoUsuarioStatusController : ControllerBase
    {
        private readonly IPontoPeriodoUsuarioStatusService _pontoPeriodoUsuarioStatusService;

        public PontoPeriodoUsuarioStatusController(IPontoPeriodoUsuarioStatusService pontoPeriodoUsuarioStatusService)
        {
            _pontoPeriodoUsuarioStatusService = pontoPeriodoUsuarioStatusService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PontoPeriodoUsuarioStatusParameters parameters)
        {
            return _pontoPeriodoUsuarioStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoPeriodoUsuarioStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PontoPeriodoUsuarioStatus Get(int codpontoPeriodoUsuarioStatus)
        {
            return _pontoPeriodoUsuarioStatusService.ObterPorCodigo(codpontoPeriodoUsuarioStatus);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] PontoPeriodoUsuarioStatus pontoPeriodoUsuarioStatus)
        {
            _pontoPeriodoUsuarioStatusService.Criar(pontoPeriodoUsuarioStatus: pontoPeriodoUsuarioStatus);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PontoPeriodoUsuarioStatus pontoPeriodoUsuarioStatus)
        {
            _pontoPeriodoUsuarioStatusService.Atualizar(pontoPeriodoUsuarioStatus: pontoPeriodoUsuarioStatus);
        }

        [HttpDelete("{codPontoPeriodoUsuarioStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPontoPeriodoUsuarioStatus)
        {
            _pontoPeriodoUsuarioStatusService.Deletar(codPontoPeriodoUsuarioStatus);
        }
    }
}
