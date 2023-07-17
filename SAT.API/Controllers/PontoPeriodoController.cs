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
    public class PontoPeriodoController : ControllerBase
    {
        private readonly IPontoPeriodoService _pontoPeriodoService;

        public PontoPeriodoController(IPontoPeriodoService pontoPeriodoService)
        {
            _pontoPeriodoService = pontoPeriodoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PontoPeriodoParameters parameters)
        {
            return _pontoPeriodoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoPeriodo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public PontoPeriodo Get(int codPontoPeriodo)
        {
            return _pontoPeriodoService.ObterPorCodigo(codPontoPeriodo);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] PontoPeriodo pontoPeriodo)
        {
            _pontoPeriodoService.Criar(pontoPeriodo: pontoPeriodo);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] PontoPeriodo pontoPeriodo)
        {
            _pontoPeriodoService.Atualizar(pontoPeriodo: pontoPeriodo);
        }

        [HttpDelete("{codPontoPeriodo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPontoPeriodo)
        {
            _pontoPeriodoService.Deletar(codPontoPeriodo);
        }
    }
}
