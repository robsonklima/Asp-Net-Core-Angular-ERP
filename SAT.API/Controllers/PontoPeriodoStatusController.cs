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
    public class PontoPeriodoStatusController : ControllerBase
    {
        private readonly IPontoPeriodoStatusService _pontoPeriodoStatusService;

        public PontoPeriodoStatusController(IPontoPeriodoStatusService pontoPeriodoStatusService)
        {
            _pontoPeriodoStatusService = pontoPeriodoStatusService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] PontoPeriodoStatusParameters parameters)
        {
            return _pontoPeriodoStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoPeriodoStatus}")]
        public PontoPeriodoStatus Get(int codPontoPeriodoStatus)
        {
            return _pontoPeriodoStatusService.ObterPorCodigo(codPontoPeriodoStatus);
        }

        [HttpPost]
        public void Post([FromBody] PontoPeriodoStatus pontoPeriodoStatus)
        {
            _pontoPeriodoStatusService.Criar(pontoPeriodoStatus: pontoPeriodoStatus);
        }

        [HttpPut]
        public void Put([FromBody] PontoPeriodoStatus pontoPeriodoStatus)
        {
            _pontoPeriodoStatusService.Atualizar(pontoPeriodoStatus: pontoPeriodoStatus);
        }

        [HttpDelete("{codPontoPeriodoStatus}")]
        public void Delete(int codPontoPeriodoStatus)
        {
            _pontoPeriodoStatusService.Deletar(codPontoPeriodoStatus);
        }
    }
}
