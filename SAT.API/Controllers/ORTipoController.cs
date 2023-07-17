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
    public class ORTipoController : ControllerBase
    {
        private readonly IORTipoService _orTipoService;

        public ORTipoController(IORTipoService orTipoService)
        {
            _orTipoService = orTipoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ORTipoParameters parameters)
        {
            return _orTipoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoOR}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ORTipo Get(int codTipoOR)
        {
            return _orTipoService.ObterPorCodigo(codTipoOR);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ORTipo tipo)
        {
            _orTipoService.Criar(tipo);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ORTipo tipo)
        {
            _orTipoService.Atualizar(tipo);
        }

        [HttpDelete("{codTipoOR}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codTipoOR)
        {
            _orTipoService.Deletar(codTipoOR);
        }
    }
}
