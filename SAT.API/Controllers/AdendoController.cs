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
    public class AdendoController : ControllerBase
    {
        private IAdendoService _AdendoService;

        public AdendoController(IAdendoService AdendoService)
        {
            _AdendoService = AdendoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AdendoParameters parameters)
        {
            return _AdendoService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodAdendo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Adendo Get(int CodAdendo)
        {
            return _AdendoService.ObterPorCodigo(CodAdendo);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Adendo Post([FromBody] Adendo adendo)
        {
            return _AdendoService.Criar(adendo);
        }

        [HttpPut("{codAdendo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public Adendo Put(int codAdendo, [FromBody] Adendo adendo)
        {
            return _AdendoService.Atualizar(adendo);
        }

        [HttpDelete("{CodAdendo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public Adendo Delete(int CodAdendo)
        {
            return _AdendoService.Deletar(CodAdendo);
        }
    }
}
