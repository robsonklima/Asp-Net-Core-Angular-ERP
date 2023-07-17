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
    public class PecaController : ControllerBase
    {
        private readonly IPecaService _pecaService;

        public PecaController(IPecaService pecaService)
        {
            _pecaService = pecaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PecaParameters parameters)
        {
            return _pecaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPeca}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Peca Get(int codPeca)
        {
            return _pecaService.ObterPorCodigo(codPeca);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] Peca peca)
        {
            _pecaService.Criar(peca);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] Peca peca)
        {
            _pecaService.Atualizar(peca);
        }

        [HttpDelete("{codPeca}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPeca)
        {
            _pecaService.Deletar(codPeca);
        }

        [HttpGet("export")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public IActionResult ExportToExcel([FromQuery] PecaParameters parameters)
        {
            return _pecaService.ExportToExcel(parameters);
        }
    }
}