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
    public class MRPLogixController : ControllerBase
    {
        private readonly IMRPLogixService _mrpLogixService;

        public MRPLogixController(IMRPLogixService mrpLogixService)
        {
            _mrpLogixService = mrpLogixService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] MRPLogixParameters parameters)
        {
            return _mrpLogixService.ObterPorParametros(parameters);
        }

        [HttpGet("{codMRPLogix}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public MRPLogix Get(int codMRPLogix)
        {
            return _mrpLogixService.ObterPorCodigo(codMRPLogix);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] MRPLogix mrpLogix)
        {
            _mrpLogixService.Criar(mrpLogix);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] MRPLogix mrpLogix)
        {
            _mrpLogixService.Atualizar(mrpLogix);
        }

        [HttpDelete("{codMRPLogix}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codMRPLogix)
        {
            _mrpLogixService.Deletar(codMRPLogix);
        }
    }
}