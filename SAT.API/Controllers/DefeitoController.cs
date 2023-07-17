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
    public class DefeitoController : ControllerBase
    {
        private readonly IDefeitoService _defeitoService;

        public DefeitoController(
            IDefeitoService defeitoService
        )
        {
            _defeitoService = defeitoService;
        }

        [HttpGet("{codDefeito}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Defeito Get(int codDefeito)
        {
            return _defeitoService.ObterPorCodigo(codDefeito);
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] DefeitoParameters parameters)
        {
            return _defeitoService.ObterPorParametros(parameters);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public Defeito Post([FromBody] Defeito defeito)
        {
            return _defeitoService.Criar(defeito);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Defeito defeito)
        {
            _defeitoService.Atualizar(defeito);
        }

        [HttpDelete("{codDefeito}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codDefeito)
        {
            _defeitoService.Deletar(codDefeito);
        }
    }
}
