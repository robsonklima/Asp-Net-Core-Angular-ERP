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
    public class PontoPeriodoStatusController : ControllerBase
    {
        private readonly IPontoPeriodoStatusService _pontoPeriodoStatusService;

        public PontoPeriodoStatusController(IPontoPeriodoStatusService pontoPeriodoStatusService)
        {
            _pontoPeriodoStatusService = pontoPeriodoStatusService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PontoPeriodoStatusParameters parameters)
        {
            return _pontoPeriodoStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoPeriodoStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PontoPeriodoStatus Get(int codPontoPeriodoStatus)
        {
            return _pontoPeriodoStatusService.ObterPorCodigo(codPontoPeriodoStatus);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] PontoPeriodoStatus pontoPeriodoStatus)
        {
            _pontoPeriodoStatusService.Criar(pontoPeriodoStatus: pontoPeriodoStatus);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PontoPeriodoStatus pontoPeriodoStatus)
        {
            _pontoPeriodoStatusService.Atualizar(pontoPeriodoStatus: pontoPeriodoStatus);
        }

        [HttpDelete("{codPontoPeriodoStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPontoPeriodoStatus)
        {
            _pontoPeriodoStatusService.Deletar(codPontoPeriodoStatus);
        }
    }
}
