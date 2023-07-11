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
    public class AuditoriaVeiculoController : ControllerBase
    {
        public IAuditoriaVeiculoService _auditoriaVeiculoService { get; }

        public AuditoriaVeiculoController(IAuditoriaVeiculoService auditoriaVeiculoService)
        {
            _auditoriaVeiculoService = auditoriaVeiculoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] AuditoriaVeiculoParameters parameters)
        {
            return _auditoriaVeiculoService.ObterPorParametros(parameters);
        }
        
        [HttpGet("{CodAuditoriaVeiculo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public AuditoriaVeiculo Get(int codAuditoriaVeiculo)
        {
            return _auditoriaVeiculoService.ObterPorCodigo(codAuditoriaVeiculo);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] AuditoriaVeiculo auditoriaVeiculo)
        {
            _auditoriaVeiculoService.Criar(auditoriaVeiculo);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] AuditoriaVeiculo auditoriaVeiculo)
        {
            _auditoriaVeiculoService.Atualizar(auditoriaVeiculo);
        }

        [HttpDelete("{CodAuditoriaVeiculo}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codAuditoriaVeiculo)
        {
            _auditoriaVeiculoService.Deletar(codAuditoriaVeiculo);
        }
    }
}
