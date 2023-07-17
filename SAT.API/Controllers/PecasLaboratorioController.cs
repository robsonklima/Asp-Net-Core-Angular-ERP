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
    public class PecasLaboratorioController : ControllerBase
    {
        private readonly IPecasLaboratorioService _pecasLaboratorioService;

        public PecasLaboratorioController(IPecasLaboratorioService pecasLaboratorioService)
        {
            _pecasLaboratorioService = pecasLaboratorioService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] PecasLaboratorioParameters parameters)
        {
            return _pecasLaboratorioService.ObterPorParametros(parameters);
        }
    }
}