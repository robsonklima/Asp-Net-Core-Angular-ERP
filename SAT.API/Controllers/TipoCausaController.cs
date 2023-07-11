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
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class TipoCausaController : ControllerBase
    {
        private readonly ITipoCausaService _tipoCausaService;

        public TipoCausaController(ITipoCausaService tipoCausaService)
        {
            _tipoCausaService = tipoCausaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] TipoCausaParameters parameters)
        {
            return _tipoCausaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codTipoCausa}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public TipoCausa Get(int codTipoCausa)
        {
            return _tipoCausaService.ObterPorCodigo(codTipoCausa);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] TipoCausa tipoCausa)
        {
            _tipoCausaService.Criar(tipoCausa);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] TipoCausa tipoCausa)
        {
            _tipoCausaService.Atualizar(tipoCausa);
        }

        [HttpDelete("{codTipoCausa}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codTipoCausa)
        {
            _tipoCausaService.Deletar(codTipoCausa);
        }
    }
}
