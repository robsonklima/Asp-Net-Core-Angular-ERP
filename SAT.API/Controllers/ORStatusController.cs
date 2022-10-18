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
    public class ORStatusController : ControllerBase
    {
        private readonly IORStatusService _orStatusService;

        public ORStatusController(IORStatusService orStatusService)
        {
            _orStatusService = orStatusService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] ORStatusParameters parameters)
        {
            return _orStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{codStatus}")]
        public ORStatus Get(int codStatus)
        {
            return _orStatusService.ObterPorCodigo(codStatus);
        }

        [HttpPost]
        public void Post([FromBody] ORStatus status)
        {
            _orStatusService.Criar(status);
        }

        [HttpPut]
        public void Put([FromBody] ORStatus status)
        {
            _orStatusService.Atualizar(status);
        }

        [HttpDelete("{codStatus}")]
        public void Delete(int codStatus)
        {
            _orStatusService.Deletar(codStatus);
        }
    }
}
