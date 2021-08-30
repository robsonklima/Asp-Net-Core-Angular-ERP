using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [EnableCors("CorsApi")]
    [ApiController]
    public class DespesaCartaoCombustivelController : ControllerBase
    {
        public IDespesaCartaoCombustivelService _despesaCartaoCombustivelService { get; }

        public DespesaCartaoCombustivelController(
            IDespesaCartaoCombustivelService despesaCartaoCombustivelService
        )
        {
            _despesaCartaoCombustivelService = despesaCartaoCombustivelService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] DespesaCartaoCombustivelParameters parameters)
        {
            return _despesaCartaoCombustivelService.ObterPorParametros(parameters);
        }
    }
}
