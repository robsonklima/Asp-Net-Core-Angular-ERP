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
    public class ORItemInsumoController : ControllerBase
    {
        private readonly IORItemInsumoService _orItemInsumoService;

        public ORItemInsumoController(IORItemInsumoService orItemInsumoService)
        {
            _orItemInsumoService = orItemInsumoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ORItemInsumoParameters parameters)
        {
            return _orItemInsumoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codORItemInsumo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ORItemInsumo Get(int codORItemInsumo)
        {
            return _orItemInsumoService.ObterPorCodigo(codORItemInsumo);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public ORItemInsumo Post([FromBody] ORItemInsumo insumo)
        {
            return _orItemInsumoService.Criar(insumo);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public ORItemInsumo Put([FromBody] ORItemInsumo insumo)
        {
            return _orItemInsumoService.Atualizar(insumo);
        }

        [HttpDelete("{codORItemInsumo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codORItemInsumo)
        {
            _orItemInsumoService.Deletar(codORItemInsumo);
        }
    }
}
