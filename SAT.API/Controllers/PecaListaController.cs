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
    public class PecaListaController : ControllerBase
    {
        private readonly IPecaListaService _pecaListaService;

        public PecaListaController(IPecaListaService pecaListaService)
        {
            _pecaListaService = pecaListaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] PecaListaParameters parameters)
        {
            return _pecaListaService.ObterPorParametros(parameters);
        }
    }
}
