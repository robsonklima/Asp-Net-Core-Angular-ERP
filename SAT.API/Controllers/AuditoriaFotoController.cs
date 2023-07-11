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
    public class AuditoriaFotoController : ControllerBase
    {
        public IAuditoriaFotoService _auditoriaFotoService { get; }

        public AuditoriaFotoController(IAuditoriaFotoService auditoriaFotoService)
        {
            _auditoriaFotoService = auditoriaFotoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AuditoriaFotoParameters parameters)
        {
            return _auditoriaFotoService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodAuditoriaFoto}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public AuditoriaFoto Get(int codAuditoriaFoto)
        {
            return _auditoriaFotoService.ObterPorCodigo(codAuditoriaFoto);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] AuditoriaFoto auditoriaFoto)
        {
            _auditoriaFotoService.Criar(auditoriaFoto);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] AuditoriaFoto auditoriaFoto)
        {
            _auditoriaFotoService.Atualizar(auditoriaFoto);
        }

        [HttpDelete("{CodAuditoriaFoto}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codAuditoriaFoto)
        {
            _auditoriaFotoService.Deletar(codAuditoriaFoto);
        }
    }
}
