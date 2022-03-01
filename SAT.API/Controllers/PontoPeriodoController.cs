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
    public class PontoPeriodoController : ControllerBase
    {
        private readonly IPontoPeriodoService _pontoPeriodoService;

        public PontoPeriodoController(IPontoPeriodoService pontoPeriodoService)
        {
            _pontoPeriodoService = pontoPeriodoService;
        }

        [Authorize(Roles = "3,35,82")]
        [HttpGet]
        public ListViewModel Get([FromQuery] PontoPeriodoParameters parameters)
        {
            return _pontoPeriodoService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPontoPeriodo}")]
        public PontoPeriodo Get(int codPontoPeriodo)
        {
            return _pontoPeriodoService.ObterPorCodigo(codPontoPeriodo);
        }

        [HttpPost]
        [Authorize(Roles = "44")]
        public void Post([FromBody] PontoPeriodo pontoPeriodo)
        {
            _pontoPeriodoService.Criar(pontoPeriodo: pontoPeriodo);
        }

        [HttpPut]
        [Authorize(Roles = "44")]
        public void Put([FromBody] PontoPeriodo pontoPeriodo)
        {
            _pontoPeriodoService.Atualizar(pontoPeriodo: pontoPeriodo);
        }

        [HttpDelete("{codPontoPeriodo}")]
        [Authorize(Roles = "44")]
        public void Delete(int codPontoPeriodo)
        {
            _pontoPeriodoService.Deletar(codPontoPeriodo);
        }
    }
}
