using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class DespesaConfiguracaoController : ControllerBase
    {
        private readonly IDespesaConfiguracaoService _despesaService;

        public DespesaConfiguracaoController(IDespesaConfiguracaoService despesaService)
        {
            _despesaService = despesaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaConfiguracaoParameters parameters)
        {
            return _despesaService.ObterPorParametros(parameters);
        }
    }
}