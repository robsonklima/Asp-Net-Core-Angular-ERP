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
    public class PecaStatusController : ControllerBase
    {
        private readonly IPecaStatusService _pecaStatusService;

        public PecaStatusController(IPecaStatusService pecaStatusService)
        {
            _pecaStatusService = pecaStatusService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] PecaStatusParameters parameters)
        {
            return _pecaStatusService.ObterPorParametros(parameters);
        }

        [HttpGet("{codPecaStatus}")]
        public PecaStatus Get(int codPecaStatus)
        {
            return _pecaStatusService.ObterPorCodigo(codPecaStatus);
        }

        [HttpPost]
        public void Post([FromBody] PecaStatus pecaStatus)
        {
            _pecaStatusService.Criar(pecaStatus);
        }

        [HttpPut]
        public void Put([FromBody] PecaStatus pecaStatus)
        {
            _pecaStatusService.Atualizar(pecaStatus);
        }

        [HttpDelete("{codPecaStatus}")]
        public void Delete(int codPecaStatus)
        {
            _pecaStatusService.Deletar(codPecaStatus);
        }

        [HttpGet("export")]
        public IActionResult ExportToExcel([FromQuery] PecaStatusParameters parameters)
        {
            return _pecaStatusService.ExportToExcel(parameters);
        }
    }
}