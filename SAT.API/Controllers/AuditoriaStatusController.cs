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
    public class AuditoriaStatusController : ControllerBase
    {
        public IAuditoriaStatusService _auditoriaStatusService { get; }

        public AuditoriaStatusController(IAuditoriaStatusService auditoriaStatusService)
        {
            _auditoriaStatusService = auditoriaStatusService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AuditoriaStatusParameters parameters)
        {
            return _auditoriaStatusService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodAuditoriaStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public AuditoriaStatus Get(int codAuditoriaStatus)
        {
            return _auditoriaStatusService.ObterPorCodigo(codAuditoriaStatus);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] AuditoriaStatus auditoriaStatus)
        {
            _auditoriaStatusService.Criar(auditoriaStatus);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] AuditoriaStatus auditoriaStatus)
        {
            _auditoriaStatusService.Atualizar(auditoriaStatus);
        }

        [HttpDelete("{CodAuditoriaStatus}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codAuditoriaStatus)
        {
            _auditoriaStatusService.Deletar(codAuditoriaStatus);
        }
    }
}
