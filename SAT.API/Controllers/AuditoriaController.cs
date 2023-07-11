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
    public class AuditoriaController : ControllerBase
    {
        public IAuditoriaService _auditoriaService { get; }

        public AuditoriaController(IAuditoriaService auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AuditoriaParameters parameters)
        {
            return _auditoriaService.ObterPorParametros(parameters);
        }

        [HttpGet("View")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel GetView([FromQuery] AuditoriaParameters parameters)
        {
            return _auditoriaService.ObterPorView(parameters);
        }
        
        [HttpGet("{CodAuditoria}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Auditoria Get(int codAuditoria)
        {
            return _auditoriaService.ObterPorCodigo(codAuditoria);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Auditoria auditoria)
        {
            _auditoriaService.Criar(auditoria);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Auditoria auditoria)
        {
            _auditoriaService.Atualizar(auditoria);
        }

        [HttpDelete("{CodAuditoria}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codAuditoria)
        {
            _auditoriaService.Deletar(codAuditoria);
        }
    }
}
