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
    public class PaisController : ControllerBase
    {
        private readonly IPaisService _paisService;

        public PaisController(IPaisService paisService)
        {
            _paisService = paisService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PaisParameters parameters)
        {
            return _paisService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPais}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Pais Get(int codPais)
        {
            return _paisService.ObterPorCodigo(codPais);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Pais pais)
        {
            _paisService.Criar(pais);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Pais pais)
        {
            _paisService.Atualizar(pais);
        }

        [HttpDelete("{codPais}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPais)
        {
            _paisService.Deletar(codPais);
        }
    }
}
