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
    public class DispBBBloqueioOSController : ControllerBase
    {
        private readonly IDispBBBloqueioOSService _dispBBBloqueioOSService;

        public DispBBBloqueioOSController(IDispBBBloqueioOSService dispBBBloqueioOSService)
        {
            _dispBBBloqueioOSService = dispBBBloqueioOSService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] DispBBBloqueioOSParameters parameters)
        {
            return _dispBBBloqueioOSService.ObterPorParametros(parameters);
        }

        [HttpGet("{codDispBBBloqueioOS}")]
        public DispBBBloqueioOS Get(int codDispBBBloqueioOS)
        {
            return _dispBBBloqueioOSService.ObterPorCodigo(codDispBBBloqueioOS);
        }

        [HttpPost]
        public DispBBBloqueioOS Post([FromBody] DispBBBloqueioOS dispBBBloqueioOS)
        {
            return _dispBBBloqueioOSService.Criar(dispBBBloqueioOS);
        }

        [HttpPut]
        public void Put([FromBody] DispBBBloqueioOS dispBBBloqueioOS)
        {
            _dispBBBloqueioOSService.Atualizar(dispBBBloqueioOS);
        }

        [HttpDelete("{codDispBBBloqueioOS}")]
        public void Delete(int codDispBBBloqueioOS)
        {
            _dispBBBloqueioOSService.Deletar(codDispBBBloqueioOS);
        }
    }
}
