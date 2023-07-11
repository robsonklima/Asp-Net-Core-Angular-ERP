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
    public class AuditoriaVeiculoAcessorioController : ControllerBase
    {
        private readonly IAuditoriaVeiculoAcessorioService _auditoriaVeiculoAcessorioService;

        public AuditoriaVeiculoAcessorioController(IAuditoriaVeiculoAcessorioService auditoriaVeiculoAcessorioService)
        {
            _auditoriaVeiculoAcessorioService = auditoriaVeiculoAcessorioService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AuditoriaVeiculoAcessorioParameters parameters)
        {
            return _auditoriaVeiculoAcessorioService.ObterPorParametros(parameters);
        }

        [HttpGet("{codAuditoriaVeiculoAcessorio}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public AuditoriaVeiculoAcessorio Get(int codAuditoriaVeiculoAcessorio)
        {
            return _auditoriaVeiculoAcessorioService.ObterPorCodigo(codAuditoriaVeiculoAcessorio);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio)
        {
            _auditoriaVeiculoAcessorioService.Criar(auditoriaVeiculoAcessorio);
        }

        [HttpPut("{codAuditoriaVeiculoAcessorio}")]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] AuditoriaVeiculoAcessorio auditoriaVeiculoAcessorio)
        {
            _auditoriaVeiculoAcessorioService.Atualizar(auditoriaVeiculoAcessorio);
        }

        [HttpDelete("{codAuditoriaVeiculoAcessorio}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codAuditoriaVeiculoAcessorio)
        {
            _auditoriaVeiculoAcessorioService.Deletar(codAuditoriaVeiculoAcessorio);
        }
    }
}
