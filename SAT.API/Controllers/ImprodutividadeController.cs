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
    public class ImprodutividadeController : ControllerBase
    {
        public IImprodutividadeService _improdutividadeService { get; }

        public ImprodutividadeController(IImprodutividadeService improdutividadeService)
        {
            _improdutividadeService = improdutividadeService;
        }

        [HttpGet]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public ListViewModel Get([FromQuery] ImprodutividadeParameters parameters)
        {
            return _improdutividadeService.ObterPorParametros(parameters);
        }
       
        [HttpGet("{CodImprodutividade}")]
        [ClaimRequirement(ClaimTypes.Role, "CanReadResource")]
        public Improdutividade Get(int codImprodutividade)
        {
            return _improdutividadeService.ObterPorCodigo(codImprodutividade);
        }

    }
}
