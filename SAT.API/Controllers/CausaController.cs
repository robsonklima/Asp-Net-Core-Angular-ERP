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
    public class CausaController : ControllerBase
    {
        private readonly ICausaService _causaService;

        public CausaController(ICausaService causaService)
        {
            _causaService = causaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] CausaParameters parameters)
        {
            return _causaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codCausa}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Causa Get(int codCausa)
        {
            return _causaService.ObterPorCodigo(codCausa);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Causa causa)
        {
            _causaService.Criar(causa);
        }

        [HttpPut("{codCausa}")]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Causa causa)
        {
            _causaService.Atualizar(causa);
        }

        [HttpDelete("{codCausa}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codCausa)
        {
            _causaService.Deletar(codCausa);
        }
    }
}
