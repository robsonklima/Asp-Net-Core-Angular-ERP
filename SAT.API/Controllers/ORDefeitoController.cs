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
    public class ORDefeitoController : ControllerBase
    {
        private readonly IORDefeitoService _orDefeitoService;

        public ORDefeitoController(IORDefeitoService orDefeitoService)
        {
            _orDefeitoService = orDefeitoService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ORDefeitoParameters parameters)
        {
            return _orDefeitoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codDefeito}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ORDefeito Get(int codDefeito)
        {
            return _orDefeitoService.ObterPorCodigo(codDefeito);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ORDefeito orDefeito)
        {
            _orDefeitoService.Criar(orDefeito);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ORDefeito orDefeito)
        {
            _orDefeitoService.Atualizar(orDefeito);
        }

        [HttpDelete("{codDefeito}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codDefeito)
        {
            _orDefeitoService.Deletar(codDefeito);
        }
    }
}
