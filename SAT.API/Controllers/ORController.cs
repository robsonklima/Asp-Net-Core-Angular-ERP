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
    public class ORController : ControllerBase
    {
        private readonly IORService _orService;

        public ORController(IORService orService)
        {
            _orService = orService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ORParameters parameters)
        {
            return _orService.ObterPorParametros(parameters);
        }

        [HttpGet("{codOR}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public OR Get(int codOR)
        {
            return _orService.ObterPorCodigo(codOR);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] OR or)
        {
            _orService.Criar(or);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] OR or)
        {
            _orService.Atualizar(or);
        }

        [HttpDelete("{codOR}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codOR)
        {
            _orService.Deletar(codOR);
        }
    }
}
