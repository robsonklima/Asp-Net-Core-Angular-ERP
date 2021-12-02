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
    public class DespesaAdiantamentoTipoController : ControllerBase
    {
        private readonly IDespesaAdiantamentoTipoService _adiantamentoTipoService;

        public DespesaAdiantamentoTipoController(IDespesaAdiantamentoTipoService adiantamentoTipoService)
        {
            _adiantamentoTipoService = adiantamentoTipoService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaAdiantamentoTipoParameters parameters)
        {
            return _adiantamentoTipoService.ObterPorParametros(parameters);
        }

    }
}