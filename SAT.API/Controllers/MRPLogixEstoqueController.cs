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
    public class MRPLogixEstoqueController : ControllerBase
    {
        private readonly IMRPLogixEstoqueService _mrpLogixService;

        public MRPLogixEstoqueController(IMRPLogixEstoqueService mrpLogixService)
        {
            _mrpLogixService = mrpLogixService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] MRPLogixEstoqueParameters parameters)
        {
            return _mrpLogixService.ObterPorParametros(parameters);
        }

        [HttpGet("{codMRPLogixEstoque}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public MRPLogixEstoque Get(int codMRPLogixEstoque)
        {
            return _mrpLogixService.ObterPorCodigo(codMRPLogixEstoque);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] MRPLogixEstoque mrpLogix)
        {
            _mrpLogixService.Criar(mrpLogix);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] MRPLogixEstoque mrpLogix)
        {
            _mrpLogixService.Atualizar(mrpLogix);
        }

        [HttpDelete("{codMRPLogixEstoque}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codMRPLogixEstoque)
        {
            _mrpLogixService.Deletar(codMRPLogixEstoque);
        }
    }
}