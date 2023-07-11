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
    public class AutorizadaController : ControllerBase
    {
        private readonly IAutorizadaService _autorizadaService;

        public AutorizadaController(IAutorizadaService autorizadaService)
        {
            _autorizadaService = autorizadaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AutorizadaParameters parameters)
        {
            return _autorizadaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codAutorizada}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Autorizada Get(int codAutorizada)
        {
            return _autorizadaService.ObterPorCodigo(codAutorizada);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Autorizada autorizada)
        {
            _autorizadaService.Criar(autorizada: autorizada);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Autorizada autorizada)
        {
            _autorizadaService.Atualizar(autorizada: autorizada);
        }

        [HttpDelete("{codAutorizada}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codAutorizada)
        {
            _autorizadaService.Deletar(codAutorizada);
        }
    }
}
