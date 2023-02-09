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
    public class OSBancadaController : ControllerBase
    {
        private readonly IOSBancadaService _OSBancadaService;

        public OSBancadaController(IOSBancadaService OSBancadaService)
        {
            _OSBancadaService = OSBancadaService;
        }

        [HttpGet]
        public ListViewModel Get([FromQuery] OSBancadaParameters parameters)
        {
            return _OSBancadaService.ObterPorParametros(parameters);
        }

        [HttpGet("{CodOsbancada}")]
        public OSBancada Get(int CodOsbancada)
        {
            return _OSBancadaService.ObterPorCodigo(CodOsbancada);
        }

        [HttpPost]
        public OSBancada Post([FromBody] OSBancada OSBancada)
        {
            return _OSBancadaService.Criar(OSBancada);
        }

        [HttpPut]
        public void Put([FromBody] OSBancada OSBancada)
        {
            _OSBancadaService.Atualizar(OSBancada);
        }

        [HttpDelete("{CodOsbancada}")]
        public void Delete(int codOsbancada)
        {
            _OSBancadaService.Deletar(codOsbancada);
        }
    }
}
