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
    public class ClientePecaGenericaController : ControllerBase
    {
        private readonly IClientePecaGenericaService _clientePecaGenericaService;

        public ClientePecaGenericaController(IClientePecaGenericaService clientePecaGenericaService)
        {
            _clientePecaGenericaService = clientePecaGenericaService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ClientePecaGenericaParameters parameters)
        {
            return _clientePecaGenericaService.ObterPorParametros(parameters);
        }

        [HttpGet("{codClientePecaGenerica}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ClientePecaGenerica Get(int codClientePecaGenerica)
        {
            return _clientePecaGenericaService.ObterPorCodigo(codClientePecaGenerica);
        }

        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanAddResource")]
        public void Post([FromBody] ClientePecaGenerica pecaGenerica)
        {
            _clientePecaGenericaService.Criar(pecaGenerica);
        }

        [HttpPut]
        [ClaimRequirement(ClaimTypes.Role, "CanEditResource")]
        public void Put([FromBody] ClientePecaGenerica pecaGenerica)
        {
            _clientePecaGenericaService.Atualizar(pecaGenerica);
        }

        [HttpDelete("{codPecaGenerica}")]
        [ClaimRequirement(ClaimTypes.Role, "CanDeleteResource")]
        public void Delete(int codPecaGenerica)
        {
            _clientePecaGenericaService.Deletar(codPecaGenerica);
        }

        [HttpGet("export")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public IActionResult ExportToExcel([FromQuery] ClientePecaGenericaParameters parameters)
        {
            return _clientePecaGenericaService.ExportToExcel(parameters);
        }
    }
}