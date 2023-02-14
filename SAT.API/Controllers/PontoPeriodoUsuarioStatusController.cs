using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SAT.MODELS.Entities.Params;
using SAT.MODELS.Entities;
using SAT.MODELS.ViewModels;
using SAT.SERVICES.Interfaces;

namespace SAT.API.Controllers
{
    [Authorize]
    [EnableCors("CorsApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class PontoPeriodoUsuarioStatusController : ControllerBase
    {
        private readonly IPontoPeriodoUsuarioStatusService _pontoPeriodoUsuarioStatusService;

        public PontoPeriodoUsuarioStatusController(IPontoPeriodoUsuarioStatusService pontoPeriodoUsuarioStatusService)
        {
            _pontoPeriodoUsuarioStatusService = pontoPeriodoUsuarioStatusService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] PontoPeriodoUsuarioStatusParameters parameters)
        {
            return _pontoPeriodoUsuarioStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoPeriodoUsuarioStatus}")]
        public PontoPeriodoUsuarioStatus Get(int codpontoPeriodoUsuarioStatus)
        {
            return _pontoPeriodoUsuarioStatusService.ObterPorCodigo(codpontoPeriodoUsuarioStatus);
        }

        [HttpPost]
        public void Post([FromBody] PontoPeriodoUsuarioStatus pontoPeriodoUsuarioStatus)
        {
            _pontoPeriodoUsuarioStatusService.Criar(pontoPeriodoUsuarioStatus: pontoPeriodoUsuarioStatus);
        }

        [HttpPut]
        public void Put([FromBody] PontoPeriodoUsuarioStatus pontoPeriodoUsuarioStatus)
        {
            _pontoPeriodoUsuarioStatusService.Atualizar(pontoPeriodoUsuarioStatus: pontoPeriodoUsuarioStatus);
        }

        [HttpDelete("{codPontoPeriodoUsuarioStatus}")]
        public void Delete(int codPontoPeriodoUsuarioStatus)
        {
            _pontoPeriodoUsuarioStatusService.Deletar(codPontoPeriodoUsuarioStatus);
        }
    }
}
