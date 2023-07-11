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
    public class AuditoriaVeiculoTanqueController : ControllerBase
    {
        private readonly IAuditoriaVeiculoTanqueService _auditoriaVeiculoTanqueService;

        public AuditoriaVeiculoTanqueController(IAuditoriaVeiculoTanqueService auditoriaVeiculoTanqueService)
        { 
            _auditoriaVeiculoTanqueService = auditoriaVeiculoTanqueService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AuditoriaVeiculoTanqueParameters parameters)
        {
            return _auditoriaVeiculoTanqueService.ObterPorParametros(parameters);
        }

        [HttpGet("{codAuditoriaVeiculoTanque}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public AuditoriaVeiculoTanque Get(int codAuditoriaVeiculoTanque)
        {
            return _auditoriaVeiculoTanqueService.ObterPorCodigo(codAuditoriaVeiculoTanque);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] AuditoriaVeiculoTanque auditoriaVeiculoTanque)
        {
            _auditoriaVeiculoTanqueService.Criar(auditoriaVeiculoTanque);
        }

        [HttpPut("{codAuditoriaVeiculoTanque}")]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] AuditoriaVeiculoTanque auditoriaVeiculoTanque)
        {
            _auditoriaVeiculoTanqueService.Atualizar(auditoriaVeiculoTanque);
        }

        [HttpDelete("{codAuditoriaVeiculoTanque}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codAuditoriaVeiculoTanque)
        {
            _auditoriaVeiculoTanqueService.Deletar(codAuditoriaVeiculoTanque);
        }
    }
}
