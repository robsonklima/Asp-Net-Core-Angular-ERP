using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class ExportacaoController : ControllerBase
    {
        private IExportacaoService _exService;
        public ExportacaoController(IExportacaoService exService)
        {
			_exService = exService;
        }
		
        [HttpPost]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public IActionResult Exportar([FromBody] Exportacao exportacao)
        {
			return _exService.Exportar(exportacao);
        }
    }
}
